using Umniah.Backend.Data.Enums;

namespace Umniah.Backend.Data;

// One row only, always under SingletonId, so two first-time saves can't create two rows.
public class SiteSetting : BaseModel
{
    public static readonly Guid SingletonId = new("00000000-0000-0000-0000-000000000001");
    public const int MaxTextLength = 200;

    public GalleryTheme GalleryTheme { get; set; }

    // Custom banner message, independent of the theme and shown in every theme.
    public string? BannerTextEn { get; set; }
    public string? BannerTextAr { get; set; }

    // UTC. After this moment the banner message is no longer shown. Null means it never expires.
    public DateTime? BannerExpiresAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
