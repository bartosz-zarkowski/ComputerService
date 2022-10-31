namespace ComputerService.Entities;

public class Accessory : IEntity
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string Name { get; set; }
    public Guid OrderId { get; set; }
    public virtual Order Order { get; set; }
}
