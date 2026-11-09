using Microsoft.EntityFrameworkCore;
using Umniah.Backend.Data;
using Umniah.Backend.Data.Context;
using Umniah.Backend.Data.Enums;
using Umniah.Backend.DTOs;
using Umniah.Backend.DTOs.Input;
using Umniah.Backend.DTOs.Output;
using Umniah.Backend.Interfaces;

namespace Umniah.Backend.Services;

public class SiteSettingsService(UmniahDbContext _context) : ISiteSettingsService
{
    // Built-in hero message of each theme, shown until one is saved for it. Default has none.
    private static readonly Dictionary<GalleryTheme, (string En, string Ar)> DefaultHeroTexts = new()
    {
        [GalleryTheme.Christmas] = ("Order early for Christmas delivery", "اطلب مبكراً لضمان التوصيل في عيد الميلاد"),
        [GalleryTheme.Valentine] = ("Pre-order your Valentine's bouquet today", "احجز باقة عيد الحب اليوم"),
        [GalleryTheme.Graduation] = ("Celebrate your graduate with flowers", "احتفل بتخرّج أحبّائك بالزهور"),
        [GalleryTheme.Wedding] = ("Book your wedding flowers early", "احجز زهور زفافك مبكراً"),
    };

    public async Task<ServiceResponse<OutputGalleryTheme>> GetGalleryTheme()
    {
        // No row yet means nobody has picked a theme: that is the Default theme, not an error.
        var theme = await _context.SiteSettings
            .AsNoTracking()
            .Where(s => s.Id == SiteSetting.SingletonId)
            .Select(s => (GalleryTheme?)s.GalleryTheme)
            .FirstOrDefaultAsync() ?? GalleryTheme.Default;

        return new ServiceResponse<OutputGalleryTheme>(true, "Gallery theme retrieved successfully",
            new OutputGalleryTheme { Theme = theme });
    }

    public async Task<ServiceResponse<bool>> SetGalleryTheme(InputGalleryTheme input)
    {
        // JSON binds any integer to an enum, so 99 would get this far.
        if (!Enum.IsDefined(input.Theme))
            return new ServiceResponse<bool>(false, "Unknown gallery theme");

        var settings = await GetOrAddSettings();
        settings.GalleryTheme = input.Theme;
        settings.UpdatedAt = DateTime.UtcNow;

        try
        {
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool>(true, "Gallery theme updated", true);
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>(false, $"Error updating gallery theme: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<OutputGallerySettings>> GetGallerySettings()
    {
        var settings = await _context.SiteSettings
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == SiteSetting.SingletonId);

        var output = new OutputGallerySettings { Theme = settings?.GalleryTheme ?? GalleryTheme.Default };

        // Compared here rather than in SQL, so it behaves the same on Postgres and SQLite.
        var expiresAt = AsUtc(settings?.BannerExpiresAt);
        if (settings != null && (expiresAt == null || expiresAt > DateTime.UtcNow))
        {
            output.BannerTextEn = settings.BannerTextEn;
            output.BannerTextAr = settings.BannerTextAr;
            output.BannerExpiresAt = expiresAt;
        }

        if (DefaultHeroTexts.TryGetValue(output.Theme, out var fallback))
        {
            var saved = await _context.GalleryThemeTexts
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Theme == output.Theme);
            output.HeroTextEn = saved?.HeroTextEn ?? fallback.En;
            output.HeroTextAr = saved?.HeroTextAr ?? fallback.Ar;
        }

        return new ServiceResponse<OutputGallerySettings>(true, "Gallery settings retrieved successfully", output);
    }

    public async Task<ServiceResponse<OutputGalleryText>> GetGalleryText()
    {
        var settings = await _context.SiteSettings
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == SiteSetting.SingletonId);

        var output = new OutputGalleryText
        {
            BannerTextEn = settings?.BannerTextEn,
            BannerTextAr = settings?.BannerTextAr,
            BannerExpiresAt = AsUtc(settings?.BannerExpiresAt)
        };
        return new ServiceResponse<OutputGalleryText>(true, "Gallery text retrieved successfully", output);
    }

    public async Task<ServiceResponse<bool>> SetGalleryText(InputGalleryText input)
    {
        if (TooLong(input.BannerTextEn, input.BannerTextAr))
            return new ServiceResponse<bool>(false, $"Each message can be at most {SiteSetting.MaxTextLength} characters");

        var bannerTextEn = Clean(input.BannerTextEn);
        var bannerTextAr = Clean(input.BannerTextAr);
        var hasMessage = bannerTextEn != null || bannerTextAr != null;

        // A time without an offset is taken as UTC; one with an offset arrives as UTC or local and is converted.
        var expiresAt = input.BannerExpiresAt switch
        {
            null => (DateTime?)null,
            { Kind: DateTimeKind.Local } local => local.ToUniversalTime(),
            var other => DateTime.SpecifyKind(other.Value, DateTimeKind.Utc)
        };
        if (hasMessage && expiresAt <= DateTime.UtcNow)
            return new ServiceResponse<bool>(false, "The expiry time must be in the future");

        var settings = await GetOrAddSettings();
        settings.BannerTextEn = bannerTextEn;
        settings.BannerTextAr = bannerTextAr;
        // Clearing the message clears its expiry too, so an old expiry can't hide the next message.
        settings.BannerExpiresAt = hasMessage ? expiresAt : null;
        settings.UpdatedAt = DateTime.UtcNow;

        try
        {
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool>(true, "Gallery text updated", true);
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>(false, $"Error updating gallery text: {ex.Message}");
        }
    }

    public async Task<ServiceResponse<List<OutputThemeHeroText>>> GetThemeHeroTexts()
    {
        var saved = await _context.GalleryThemeTexts
            .AsNoTracking()
            .ToDictionaryAsync(t => t.Theme);

        var output = DefaultHeroTexts.Select(d => new OutputThemeHeroText
        {
            Theme = d.Key,
            HeroTextEn = saved.GetValueOrDefault(d.Key)?.HeroTextEn,
            HeroTextAr = saved.GetValueOrDefault(d.Key)?.HeroTextAr,
            DefaultHeroTextEn = d.Value.En,
            DefaultHeroTextAr = d.Value.Ar
        }).ToList();

        return new ServiceResponse<List<OutputThemeHeroText>>(true, "Theme hero texts retrieved successfully", output);
    }

    public async Task<ServiceResponse<bool>> SetThemeHeroText(GalleryTheme theme, InputThemeHeroText input)
    {
        // Covers undefined values and Default, which has no hero message.
        if (!DefaultHeroTexts.ContainsKey(theme))
            return new ServiceResponse<bool>(false, "This theme has no hero message");

        if (TooLong(input.HeroTextEn, input.HeroTextAr))
            return new ServiceResponse<bool>(false, $"Each message can be at most {GalleryThemeText.MaxTextLength} characters");

        var row = await _context.GalleryThemeTexts.FindAsync(theme);
        if (row == null)
        {
            row = new GalleryThemeText { Theme = theme };
            _context.GalleryThemeTexts.Add(row);
        }

        row.HeroTextEn = Clean(input.HeroTextEn);
        row.HeroTextAr = Clean(input.HeroTextAr);
        row.UpdatedAt = DateTime.UtcNow;

        try
        {
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool>(true, "Theme hero message updated", true);
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>(false, $"Error updating theme hero message: {ex.Message}");
        }
    }

    private async Task<SiteSetting> GetOrAddSettings()
    {
        var settings = await _context.SiteSettings.FindAsync(SiteSetting.SingletonId);
        if (settings != null)
            return settings;

        settings = new SiteSetting { Id = SiteSetting.SingletonId };
        _context.SiteSettings.Add(settings);
        return settings;
    }

    private static bool TooLong(params string?[] texts)
        => texts.Any(t => t?.Trim().Length > SiteSetting.MaxTextLength);

    private static string? Clean(string? text) => string.IsNullOrWhiteSpace(text) ? null : text.Trim();

    // Stored values come back with no Kind; marking them UTC makes the JSON carry a "Z", so browsers don't read them as local time.
    private static DateTime? AsUtc(DateTime? value)
        => value == null ? null : DateTime.SpecifyKind(value.Value, DateTimeKind.Utc);
}
