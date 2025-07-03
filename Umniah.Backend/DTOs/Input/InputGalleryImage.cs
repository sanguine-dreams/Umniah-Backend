using Umniah.Backend.Data.Enums;

namespace Umniah.Backend.DTOs.Input;

public class InputGalleryImage
{
    public string imageFile { get; set; }
    public string Caption { get; set; } 
    public List<string> Tags { get; set; }
}