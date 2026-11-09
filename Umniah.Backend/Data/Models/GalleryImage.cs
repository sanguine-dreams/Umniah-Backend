using Umniah.Backend.Data.Enums;

namespace Umniah.Backend.Data;

public class GalleryImage : BaseModel
{
    public Guid StoredFileId { get; set; }
    public StoredFile StoredFile { get; set; } = null!;
    public string? Caption { get; set; } 
    public List<ImageTags>? Tags { get; set; }
}
