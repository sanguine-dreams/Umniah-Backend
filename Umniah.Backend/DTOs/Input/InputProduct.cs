using Umniah.Backend.Data.Enums;

namespace Umniah.Backend.DTOs.Input;

public class InputProduct
{
    public ProductType ProductType { get; set; }
    // public GiftCategory? GiftCategory { get; set; }
    public required string Name { get; set; }
    public int Quantity { get; set; }
    public decimal UnitCost  { get; set; }
    public decimal UnitPrice { get; set; }
 
}