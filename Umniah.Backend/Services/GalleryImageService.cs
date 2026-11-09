using Microsoft.EntityFrameworkCore;
using Umniah.Backend.Data;
using Umniah.Backend.Data.Context;
using Umniah.Backend.DTOs;
using Umniah.Backend.DTOs.Input;
using Umniah.Backend.DTOs.Output;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Services;

public class GalleryImageService(UmniahDbContext _context, IFileLedger _fileLedger) : ICrudService<InputGalleryImage, OutputGalleryImage>
{
    public async Task<ServiceResponse<bool>> Create(InputGalleryImage input)
    {
        if (input.Tags == null || input.Tags.Count == 0)
            return new ServiceResponse<bool>(false, "At least one tag is required");

        if (input.File == null)
            return new ServiceResponse<bool>(false, "An image file is required");

        var upload = await _fileLedger.SaveUploadAsync(input.File);
        if (!upload.IsSuccess)
            return new ServiceResponse<bool>(false, upload.Message);

        var storedFile = upload.Data!;
        var image = new GalleryImage
        {
            StoredFile = storedFile,
            Caption = input.Caption,
            Tags = input.Tags
        };
        _context.GalleryImages.Add(image);
        _fileLedger.Confirm(storedFile);

        try
        {
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool>(true, "Image successfully created", true);
        }
        catch (Exception ex)
        {
            // The file stays Pending and the cleanup job removes it after the grace period.
            return new ServiceResponse<bool>(false, $"Error saving image: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<bool>> Edit(Guid id, InputGalleryImage input)
    {
        if (input.Tags == null || input.Tags.Count == 0)
            return new ServiceResponse<bool>(false, "At least one tag is required");

        var existingImage = await _context.GalleryImages
            .Include(g => g.StoredFile)
            .FirstOrDefaultAsync(g => g.Id == id);
        if (existingImage == null)
            return new ServiceResponse<bool>(false, "Gallery image not found");

        // The upload saves on this context, so it must run before the image is changed.
        if (input.File != null)
        {
            var upload = await _fileLedger.SaveUploadAsync(input.File);
            if (!upload.IsSuccess)
                return new ServiceResponse<bool>(false, upload.Message);

            _fileLedger.MarkForDeletion(existingImage.StoredFile);
            existingImage.StoredFile = upload.Data!;
            _fileLedger.Confirm(upload.Data!);
        }

        existingImage.Caption = input.Caption;
        existingImage.Tags = input.Tags;

        try
        {
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool>(true, "Image successfully updated", true);
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>(false, $"Error updating image: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<bool>> Delete(Guid id)
    {
        var existingImage = await _context.GalleryImages
            .Include(g => g.StoredFile)
            .FirstOrDefaultAsync(g => g.Id == id);
        if (existingImage == null)
            return new ServiceResponse<bool>(false, "Gallery image not found");

        _context.GalleryImages.Remove(existingImage);
        _fileLedger.MarkForDeletion(existingImage.StoredFile);

        try
        {
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool>(true, "Image successfully deleted", true);
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>(false, $"Error deleting image: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<OutputGalleryImage>> GetById(Guid id)
    {
        var image = await _context.GalleryImages
            .AsNoTracking()
            .Where(g => g.Id == id)
            .Select(g => new { g.Id, g.StoredFile.FileName, g.Caption, g.Tags, g.CreatedOn })
            .FirstOrDefaultAsync();
        if (image == null)
            return new ServiceResponse<OutputGalleryImage>(false, "Gallery image not found");

        var output = new OutputGalleryImage
        {
            Id = image.Id,
            ImageUrl = FileLedger.ToUrl(image.FileName),
            Caption = image.Caption,
            Tags = image.Tags,
            CreatedOn = image.CreatedOn
        };
        return new ServiceResponse<OutputGalleryImage>(true, "Image retrieved successfully", output);
    }

    public async Task<ServiceResponse<List<OutputGalleryImage>>> GetAll()
    {
        var images = await _context.GalleryImages
            .AsNoTracking()
            .OrderByDescending(g => g.CreatedOn)
            .Select(g => new { g.Id, g.StoredFile.FileName, g.Caption, g.Tags, g.CreatedOn })
            .ToListAsync();

        var output = images.Select(g => new OutputGalleryImage
        {
            Id = g.Id,
            ImageUrl = FileLedger.ToUrl(g.FileName),
            Caption = g.Caption,
            Tags = g.Tags,
            CreatedOn = g.CreatedOn
        }).ToList();
        return new ServiceResponse<List<OutputGalleryImage>>(true, "Images retrieved successfully", output);
    }
}
