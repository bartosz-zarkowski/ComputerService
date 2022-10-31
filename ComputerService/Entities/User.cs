using ComputerService.Entities.Enums;

namespace ComputerService.Entities;
public class User : IEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public UserRoleEnum Role { get; set; }

    public virtual IEnumerable<Order>? CreatedOrders { get; set; }
    public virtual IEnumerable<Order>? ServicedOrders { get; set; }
    public virtual IEnumerable<Order>? CompletedOrders { get; set; }
}
