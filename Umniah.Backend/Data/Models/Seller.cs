namespace Umniah.Backend.Data;

public class Seller : BaseModel
{
    public required string Name { get; set; }
    public string? Contact { get; set; }
    public string? Address { get; set; }
}