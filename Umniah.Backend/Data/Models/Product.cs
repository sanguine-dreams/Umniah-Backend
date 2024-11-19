using Umniah.Backend.Data.Enums;

namespace Umniah.Backend.Data;

public class Product : BaseModel
{
    public ProductType ProductType { get; set; }
    public GiftCategory? GiftCategory { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal UnitCost  { get; set; }
    public decimal UnitPrice { get; set; }
    
}