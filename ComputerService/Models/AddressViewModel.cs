using ComputerService.Entities;

namespace ComputerService.Models;

public class AddressViewModel
{
    public Guid Id { get; set; }
    public virtual Customer Customer { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Street { get; set; }
    public string StreetNumber { get; set; }
    public string? Apartment { get; set; }
}

