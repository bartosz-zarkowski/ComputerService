using ComputerService.Entities.Enums;

namespace ComputerService.Models;
public class UpdateOrderModel
{
    public OrderStatusEnum Status { get; set; }
    public string? Description { get; set; }
    public Guid? ServicedBy { get; set; }
    public Guid? CompletedBy { get; set; }
}
