using Umniah.Backend.Data;

namespace Umniah.Backend.DTOs.Input;

public class InputPurchase
{
    public required string InvoiceNumber { get; set; }
    public required InputSeller Seller { get; set; }
    public required List<InputProduct> Product { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal AdditionalCost { get; set; }
    public string? Notes { get; set; }

}