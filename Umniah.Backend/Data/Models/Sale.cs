namespace Umniah.Backend.Data;

public class Sale : BaseModel
{
    public string InvoiceNumber { get; set; }
    public List<Product> Products { get; set; }
    public decimal Total { get; set; }
    public decimal AdditionalCost { get; set; }
    public string Notes { get; set; }
}