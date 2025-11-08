using Umniah.Backend.Data;

namespace Umniah.Backend.DTOs.Input;

public class InputPurchase
{
    public string InvoiceNumber { get; set; }
    public Seller Seller { get; set; }
    public List<Product> Product { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal AdditionalCost { get; set; }
    public string Notes { get; set; }

}