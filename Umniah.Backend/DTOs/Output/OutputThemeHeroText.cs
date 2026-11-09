using Umniah.Backend.Data.Enums;

namespace Umniah.Backend.DTOs.Output;

// For the dashboard: the saved message (null when none is saved) next to the default it falls back to.
public class OutputThemeHeroText
{
    public GalleryTheme Theme { get; set; }
    public string? HeroTextEn { get; set; }
    public string? HeroTextAr { get; set; }
    public required string DefaultHeroTextEn { get; set; }
    public required string DefaultHeroTextAr { get; set; }
}
