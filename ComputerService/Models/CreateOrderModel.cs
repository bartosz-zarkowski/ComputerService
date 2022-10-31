namespace ComputerService.Models;
public class CreateOrderModel
{
    public Guid ClientId { get; set; }
    public string? Description { get; set; }
    public Guid? CreatedBy { get; set; }
}
