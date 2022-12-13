namespace ComputerService.ViewModels;

public class DeviceViewModel
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string Name { get; set; }
    public string? SerialNumber { get; set; }
    public string? Password { get; set; }
    public string? Condition { get; set; }
    public bool HasWarranty { get; set; }
}