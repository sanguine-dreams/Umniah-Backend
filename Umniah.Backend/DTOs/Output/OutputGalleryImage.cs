using Umniah.Backend.Data.Enums;

namespace Umniah.Backend.DTOs.Output;

public class OutputGalleryImage
{
    public string ImageFile { get; set; }
    public string Caption { get; set; } 
    public List<ImageTags> Tags { get; set; }
}