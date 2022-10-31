namespace ComputerService.Entities;
public class OrderDetails : IEntity
{
    public Guid Id { get; set; }
    public virtual Order Order { get; set; }
    public string? ServiceDescription { get; set; }
    public string? AdditionalInformation { get; set; }
    public decimal? HardwareCharges { get; set; }
    public decimal? ServiceCharges { get; set; }
}