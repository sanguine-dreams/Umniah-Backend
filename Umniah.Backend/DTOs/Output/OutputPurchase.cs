using Umniah.Backend.Data;

namespace Umniah.Backend.DTOs.Output;


public class OutputPurchase
{
    public Guid Id { get; set; }
    public required string InvoiceNumber { get; set; }
    public required Seller Seller { get; set; }
    public required List<Product> Product { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal AdditionalCost { get; set; }
    public string? Notes { get; set; }
}