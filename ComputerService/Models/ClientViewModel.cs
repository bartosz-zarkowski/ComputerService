using ComputerService.Entities;

namespace ComputerService.Models;

public class ClientViewModel
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; }
    public virtual Address? Address { get; set; }
    public virtual IEnumerable<Device>? Devices { get; set; }
    public virtual IEnumerable<Order>? Orders { get; set; }
}

