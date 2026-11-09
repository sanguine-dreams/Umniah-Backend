using Umniah.Backend.Data.Enums;

namespace Umniah.Backend.Data;

// The hero message chosen for one theme. One row per theme at most, keyed by the theme itself.
// No row, or a null field, means the theme's built-in default message is shown.
public class GalleryThemeText
{
    public const int MaxTextLength = 200;

    public GalleryTheme Theme { get; set; }
    public string? HeroTextEn { get; set; }
    public string? HeroTextAr { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
