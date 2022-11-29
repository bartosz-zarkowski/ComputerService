using ComputerService.Entities.Enums;

namespace ComputerService.Models;
public class UpdateOrderModel
{
    public string Title { get; set; }
    public OrderStatusEnum Status { get; set; }
    public string? Description { get; set; }
    public Guid? ServicedBy { get; set; }
}