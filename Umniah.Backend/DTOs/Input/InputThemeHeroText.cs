namespace Umniah.Backend.DTOs.Input;

// A theme's hero message. Empty or whitespace falls back to the theme's default message.
public class InputThemeHeroText
{
    public string? HeroTextEn { get; set; }
    public string? HeroTextAr { get; set; }
}
