using Umniah.Backend.Data.Enums;

namespace Umniah.Backend.DTOs.Input;

public class InputGalleryImage
{   
   
    public required string ImageFile { get; set; }
    public string? Caption { get; set; } 
    public List<ImageTags>? Tags { get; set; }
}