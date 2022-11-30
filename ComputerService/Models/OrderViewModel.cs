using ComputerService.Entities;
using ComputerService.Entities.Enums;

namespace ComputerService.Models;

public class OrderViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? ReceivedAt { get; set; }
    public Guid CustomerId { get; set; }
    public OrderStatusEnum Status { get; set; }
    public string? Description { get; set; }
    public virtual OrderDetails Details { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? ServicedBy { get; set; }
    public Guid? CompletedBy { get; set; }
    public virtual IEnumerable<Device>? Devices { get; set; }
    public virtual IEnumerable<OrderAccessory>? Accessories { get; set; }
}

