using System.Net.Mime;
using Umniah.Backend.Data.Enums;

namespace Umniah.Backend.Data;

public class GalleryImage : BaseModel
{
    public string imageFile { get; set; }
    public string Caption { get; set; } 
    public List<ImageTags> Tags { get; set; }
}