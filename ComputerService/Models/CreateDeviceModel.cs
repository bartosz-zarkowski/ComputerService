namespace ComputerService.Models;
public class CreateDeviceModel
{
    public string Name { get; set; }
    public string? SerialNumber { get; set; }
    public string? Password { get; set; }
    public string? Condition { get; set; }
    public bool HasWarranty { get; set; }
    public Guid ClientId { get; set; }
    public Guid OrderId { get; set; }
}
