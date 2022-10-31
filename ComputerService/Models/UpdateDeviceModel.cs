namespace ComputerService.Models;
public class UpdateDeviceModel
{
    public string Name { get; set; }
    public string? SerialNumber { get; set; }
    public string? Password { get; set; }
    public string? Condition { get; set; }
    public bool HasWarranty { get; set; }
}
