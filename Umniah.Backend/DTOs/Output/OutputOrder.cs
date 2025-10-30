namespace Umniah.Backend.DTOs.Output;

public class OutputOrder
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<Guid> ProductIds { get; set; }
}