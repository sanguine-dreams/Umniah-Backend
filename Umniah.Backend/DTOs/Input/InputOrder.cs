using Umniah.Backend.Data;

namespace Umniah.Backend.DTOs.Input;

public class InputOrder
{
     public string Name { get; set; }
    public DateTime PickUpTime { get; set; }
    public Sale Sale { get; set; }
    public string Contact { get; set; }
    public string Notes { get; set; }
    public decimal AmountDue { get; set; }
    public bool Paid { get; set; }
    public bool Done { get; set; }
}