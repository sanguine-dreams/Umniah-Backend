namespace Umniah.Backend.Data;

public class Order : BaseModel
{
    public required string Name { get; set; }
    public DateTime PickUpTime { get; set; }
    public required Sale Sale { get; set; }
    public Guid SaleId { get; set; }
    public string? Contact { get; set; }
    public string? Notes { get; set; }
    public decimal AmountDue { get; set; }
    public bool Paid { get; set; }
    public bool Done { get; set; }
}