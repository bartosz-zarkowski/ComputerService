namespace ComputerService.ViewModels;

public class CustomerViewModel
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; }
    public virtual AddressViewModel? Address { get; set; }
    public virtual IEnumerable<DeviceViewModel>? Devices { get; set; }
    public virtual IEnumerable<OrderViewModel>? Orders { get; set; }
}