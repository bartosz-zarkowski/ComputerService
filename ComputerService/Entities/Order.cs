using ComputerService.Entities.Enums;

namespace ComputerService.Entities;
public class Order : IEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }

    public Guid? ClientId { get; set; }
    public virtual Client? Client { get; set; }

    public OrderStatusEnum Status { get; set; }
    public string? Description { get; set; }
    public virtual OrderDetails Details { get; set; }

    public Guid? CreatedBy { get; set; }
    public virtual User? CreateUser { get; set; }

    public Guid? ServicedBy { get; set; }
    public virtual User? ServiceUser { get; set; }

    public Guid? CompletedBy { get; set; }
    public virtual User? CompleteUser { get; set; }

    public virtual IEnumerable<Device>? Devices { get; set; }
    public virtual IEnumerable<OrderAccessory>? Accessories { get; set; }
}
