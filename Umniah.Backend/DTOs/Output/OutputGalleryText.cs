namespace Umniah.Backend.DTOs.Output;

// For the dashboard: the stored banner message as saved, even after it has expired.
public class OutputGalleryText
{
    public string? BannerTextEn { get; set; }
    public string? BannerTextAr { get; set; }
    public DateTime? BannerExpiresAt { get; set; }
}
