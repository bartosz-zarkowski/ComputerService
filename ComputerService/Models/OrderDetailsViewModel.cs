namespace ComputerService.Models;

public class OrderDetailsViewModel
{
    public Guid Id { get; set; }
    public string? ServiceDescription { get; set; }
    public string? AdditionalInformation { get; set; }
    public decimal? HardwareCharges { get; set; }
    public decimal? ServiceCharges { get; set; }
}

