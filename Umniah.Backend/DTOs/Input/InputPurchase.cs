using System.Security.Cryptography;
using Umniah.Backend.Data;

namespace Umniah.Backend.DTOs.Input;

public class InputPurchase
{
    public string InvoiceNumber { get; set; }  = $"PUR-{RandomNumberGenerator.GetInt32(10000, 99999)}";    
    public InputSeller? InputSeller { get; set; }
    public required List<InputProduct> Product { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal AdditionalCost { get; set; }
    public string? Notes { get; set; }

}