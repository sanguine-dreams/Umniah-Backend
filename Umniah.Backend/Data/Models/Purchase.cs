namespace Umniah.Backend.Data;

public class Purchase : BaseModel
{
    public required string InvoiceNumber { get; set; }
    public required Seller Seller { get; set; }
    public Guid SellerId { get; set; }
    public required List<Product> Product { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal AdditionalCost { get; set; }
    public string? Notes { get; set; }
}