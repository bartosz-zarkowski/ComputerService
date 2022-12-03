namespace ComputerService.ViewModels;

public class OrderAccessoryViewModel
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string Name { get; set; }
    public Guid OrderId { get; set; }
}