using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Umniah.Backend.Config;
using Umniah.Backend.Data.Context;
using Umniah.Backend.Data.Enums;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Services;

public class FileLedgerCleanupJob(
    IServiceScopeFactory _scopeFactory,
    IOptions<FileLedgerOptions> _options,
    IWebHostEnvironment _environment,
    ILogger<FileLedgerCleanupJob> _logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var options = _options.Value;
        if (!options.Enabled)
        {
            _logger.LogInformation("File ledger cleanup is disabled");
            return;
        }

        using var timer = new PeriodicTimer(TimeSpan.FromMinutes(options.IntervalMinutes));
        do
        {
            try
            {
                await RemoveExpiredImagesAsync(options, stoppingToken);
                await DeleteFilesAsync(options, stoppingToken);
            }
            catch (Exception ex) when (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogError(ex, "File ledger cleanup pass failed");
            }
        }
        while (await timer.WaitForNextTickAsync(stoppingToken));
    }

    // Keeps the newest KeepPerTag images of every tag. An image survives if it is in the newest set of any of its tags.
    private async Task RemoveExpiredImagesAsync(FileLedgerOptions options, CancellationToken ct)
    {
        // Taken before the keep set is built, so an upload that lands mid-pass is never a candidate.
        var snapshot = DateTime.UtcNow;

        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<UmniahDbContext>();
        var fileLedger = scope.ServiceProvider.GetRequiredService<IFileLedger>();

        var keep = new HashSet<Guid>();
        foreach (var tag in Enum.GetValues<ImageTags>())
        {
            var newest = await context.GalleryImages
                .AsNoTracking()
                .Where(g => g.Tags!.Contains(tag))
                .OrderByDescending(g => g.CreatedOn)
                .ThenByDescending(g => g.Id)
                .Take(options.KeepPerTag)
                .Select(g => g.Id)
                .ToListAsync(ct);
            keep.UnionWith(newest);
        }
        var keepIds = keep.ToList();

        while (true)
        {
            var expired = await context.GalleryImages
                .Include(g => g.StoredFile)
                .Where(g => g.CreatedOn < snapshot
                            && g.Tags != null && g.Tags.Count > 0
                            && !keepIds.Contains(g.Id))
                .OrderBy(g => g.CreatedOn)
                .Take(options.BatchSize)
                .ToListAsync(ct);

            if (expired.Count == 0)
                break;

            foreach (var image in expired)
            {
                context.GalleryImages.Remove(image);
                fileLedger.MarkForDeletion(image.StoredFile);
            }
            await context.SaveChangesAsync(ct);
            _logger.LogInformation("Removed {Count} gallery images past the per-tag limit", expired.Count);

            if (expired.Count < options.BatchSize)
                break;
        }
    }

    // Deletes files marked Deleting and Pending files never confirmed within the grace period. Never touches Live rows.
    private async Task DeleteFilesAsync(FileLedgerOptions options, CancellationToken ct)
    {
        var passStart = DateTime.UtcNow;
        var graceCutoff = passStart.AddHours(-options.PendingGraceHours);
        var root = Path.GetFullPath(FileLedger.GetUploadsRoot(_environment));
        var rootPrefix = Path.EndsInDirectorySeparator(root) ? root : root + Path.DirectorySeparatorChar;

        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<UmniahDbContext>();

        while (true)
        {
            var rows = await context.StoredFiles
                .Where(f => f.Status == StoredFileStatus.Deleting
                            || (f.Status == StoredFileStatus.Pending && f.CreatedOn < graceCutoff))
                .Where(f => f.LastAttemptAt == null || f.LastAttemptAt < passStart)
                .OrderBy(f => f.CreatedOn)
                .Take(options.BatchSize)
                .ToListAsync(ct);

            if (rows.Count == 0)
                break;

            foreach (var row in rows)
            {
                var path = Path.GetFullPath(Path.Combine(root, row.FileName));
                if (!path.StartsWith(rootPrefix, StringComparison.Ordinal))
                {
                    row.Status = StoredFileStatus.Failed;
                    row.LastAttemptAt = DateTime.UtcNow;
                    row.LastError = "Path resolves outside the uploads folder";
                    _logger.LogError("Refused to delete {FileName}: path resolves outside the uploads folder", row.FileName);
                    continue;
                }

                try
                {
                    if (File.Exists(path))
                        File.Delete(path);
                    context.StoredFiles.Remove(row);
                }
                catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
                {
                    row.Attempts++;
                    row.LastAttemptAt = DateTime.UtcNow;
                    row.LastError = ex.Message.Length > 400 ? ex.Message[..400] : ex.Message;

                    if (row.Attempts >= options.MaxAttempts)
                    {
                        row.Status = StoredFileStatus.Failed;
                        _logger.LogError(ex, "Giving up on deleting {FileName} after {Attempts} attempts", row.FileName, row.Attempts);
                    }
                    else
                    {
                        _logger.LogWarning(ex, "Could not delete {FileName}, attempt {Attempts}", row.FileName, row.Attempts);
                    }
                }
            }
            await context.SaveChangesAsync(ct);

            if (rows.Count < options.BatchSize)
                break;
        }
    }
}
