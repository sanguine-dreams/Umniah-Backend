using Microsoft.EntityFrameworkCore;
using Umniah.Backend.Data;
using Umniah.Backend.Data.Context;
using Umniah.Backend.Data.Enums;
using Umniah.Backend.DTOs;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Services;

public class FileLedger(UmniahDbContext _context, IWebHostEnvironment _environment, ILogger<FileLedger> _logger) : IFileLedger
{
    public const string UploadsRequestPath = "/uploads";
    private const long MaxBytes = 10 * 1024 * 1024;

    public static string GetUploadsRoot(IWebHostEnvironment environment)
        => Path.Combine(environment.ContentRootPath, "wwwroot", "uploads");

    public static string ToUrl(string fileName) => $"{UploadsRequestPath}/{fileName}";

    public async Task<ServiceResponse<StoredFile>> SaveUploadAsync(IFormFile file)
    {
        if (file.Length == 0)
            return new ServiceResponse<StoredFile>(false, "The image file is empty");

        if (file.Length > MaxBytes)
            return new ServiceResponse<StoredFile>(false, "Image size cannot exceed 10MB");

        // The client's file name and content type are untrusted: the extension comes from the file's own bytes.
        var extension = await DetectImageExtensionAsync(file);
        if (extension == null)
            return new ServiceResponse<StoredFile>(false, "Only JPEG, PNG and WebP images are allowed");

        var root = GetUploadsRoot(_environment);
        Directory.CreateDirectory(root);
        var fileName = $"{Guid.NewGuid()}{extension}";
        var path = Path.Combine(root, fileName);

        await using (var stream = new FileStream(path, FileMode.CreateNew))
        {
            await file.CopyToAsync(stream);
        }

        var storedFile = new StoredFile { FileName = fileName, Status = StoredFileStatus.Pending };
        try
        {
            _context.StoredFiles.Add(storedFile);
            await _context.SaveChangesAsync();
            return new ServiceResponse<StoredFile>(true, "File stored", storedFile);
        }
        catch (Exception ex)
        {
            // No row means nothing would ever clean the file up, so remove it now.
            _context.Entry(storedFile).State = EntityState.Detached;
            TryDeleteFile(path);
            return new ServiceResponse<StoredFile>(false, $"Error recording file: {ex.Message}");
        }
    }

    public void Confirm(StoredFile storedFile)
    {
        storedFile.Status = StoredFileStatus.Live;
        storedFile.UpdatedAt = DateTime.UtcNow;
    }

    public void MarkForDeletion(StoredFile storedFile)
    {
        storedFile.Status = StoredFileStatus.Deleting;
        storedFile.UpdatedAt = DateTime.UtcNow;
    }

    private static async Task<string?> DetectImageExtensionAsync(IFormFile file)
    {
        var header = new byte[12];
        await using var stream = file.OpenReadStream();
        var read = await stream.ReadAtLeastAsync(header, header.Length, throwOnEndOfStream: false);
        return MatchImageSignature(header.AsSpan(0, read));
    }

    private static string? MatchImageSignature(ReadOnlySpan<byte> bytes)
    {
        if (bytes.StartsWith(new byte[] { 0xFF, 0xD8, 0xFF }))
            return ".jpg";
        if (bytes.StartsWith(new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }))
            return ".png";
        if (bytes.Length >= 12 && bytes[..4].SequenceEqual("RIFF"u8) && bytes[8..12].SequenceEqual("WEBP"u8))
            return ".webp";

        return null;
    }

    private void TryDeleteFile(string path)
    {
        try
        {
            File.Delete(path);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            _logger.LogWarning(ex, "Could not remove unrecorded upload {Path}", path);
        }
    }
}
