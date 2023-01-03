using ComputerService.Entities.Enums;

namespace ComputerService.ViewModels;

public class OrderViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }
    public CustomerViewModel Customer { get; set; }
    public OrderStatusEnum Status { get; set; }
    public string? Description { get; set; }
    public virtual OrderDetailsViewModel Details { get; set; }
    public UserViewModel CreateUser { get; set; }
    public UserViewModel ServiceUser { get; set; }
    public UserViewModel CompleteUser { get; set; }
    public virtual IEnumerable<DeviceViewModel>? Devices { get; set; }
    public virtual IEnumerable<OrderAccessoryViewModel>? Accessories { get; set; }
}