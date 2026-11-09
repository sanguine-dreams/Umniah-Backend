using Umniah.Backend.Data.Enums;

namespace Umniah.Backend.Data;

public class StoredFile : BaseModel
{
    public required string FileName { get; set; }
    public StoredFileStatus Status { get; set; }
    public int Attempts { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? LastAttemptAt { get; set; }
    public string? LastError { get; set; }
}
