using Umniah.Backend.Data.Enums;

namespace Umniah.Backend.DTOs.Output;

// Everything the public gallery needs in one call.
public class OutputGallerySettings
{
    public GalleryTheme Theme { get; set; }

    // Null once the banner has expired. BannerExpiresAt lets an open page hide it at the right moment.
    public string? BannerTextEn { get; set; }
    public string? BannerTextAr { get; set; }
    public DateTime? BannerExpiresAt { get; set; }

    // The active theme's hero message, already resolved to its default when none is saved.
    // Null under the Default theme, which has no hero message.
    public string? HeroTextEn { get; set; }
    public string? HeroTextAr { get; set; }
}
