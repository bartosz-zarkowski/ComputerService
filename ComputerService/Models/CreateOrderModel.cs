namespace ComputerService.Models;
public class CreateOrderModel
{
    public Guid CustomerId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
}
