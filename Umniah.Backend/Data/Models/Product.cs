using Umniah.Backend.Data.Enums;

namespace Umniah.Backend.Data;

public class Product : BaseModel
{
    public Seller? Seller { get; set; }
    public Guid? SellerId { get; set; }
    public Purchase? Purchase { get; set; }
    public Guid? PurchaseId { get; set; }
    public Sale? Sale { get; set; }
    public Guid? SaleId { get; set; }
    public ProductType ProductType { get; set; }
    // public GiftCategory? GiftCategory { get; set; }
    public required string Name { get; set; }
    public int Quantity { get; set; }
    public decimal UnitCost  { get; set; }
    public decimal UnitPrice { get; set; }
    
}