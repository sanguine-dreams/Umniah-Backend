using Umniah.Backend.Data;

namespace Umniah.Backend.DTOs.Output;

public class OutputSale
{
    public Guid Id { get; set; }
    public required string InvoiceNumber { get; set; }
    public required List<Product> Products { get; set; }
    public decimal Total { get; set; }
    public decimal AdditionalCost { get; set; }
    public string? Notes { get; set; }
}