namespace Umniah.Backend.DTOs.Input;

public class InputOrder
{
    public Guid CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<Guid> ProductIds { get; set; }
}