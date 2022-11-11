namespace ComputerService.Entities;

public class Accessory : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}