namespace Umniah.Backend.DTOs.Input;

// The custom banner message. Empty or whitespace clears it.
public class InputGalleryText
{
    public string? BannerTextEn { get; set; }
    public string? BannerTextAr { get; set; }

    // Optional. Send it with a UTC offset (e.g. "2026-11-14T18:00:00Z"); null means the message never expires.
    public DateTime? BannerExpiresAt { get; set; }
}
