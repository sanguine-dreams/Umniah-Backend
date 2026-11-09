using Umniah.Backend.Data.Enums;

namespace Umniah.Backend.DTOs.Input;

public class InputGalleryImage
{
    // Required on create. On edit, a new file replaces the current one; leave it empty to keep the image.
    public IFormFile? File { get; set; }
    public string? Caption { get; set; }
    public List<ImageTags>? Tags { get; set; }
}
