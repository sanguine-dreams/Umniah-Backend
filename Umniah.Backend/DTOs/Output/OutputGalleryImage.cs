using Umniah.Backend.Data.Enums;

namespace Umniah.Backend.DTOs.Output;

public class OutputGalleryImage
{
    public Guid Id { get; set; }
    public required string ImageUrl { get; set; }
    public string? Caption { get; set; }
    public List<ImageTags>? Tags { get; set; }
    public DateTime CreatedOn { get; set; }
}
