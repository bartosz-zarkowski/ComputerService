using ComputerService.Entities;
using ComputerService.Models;

namespace ComputerService.Interfaces;
public interface IAddressService
{
    Task<PagedList<Address>> GetAllAddressesAsync(ParametersModel parameters);
    Task<Address> GetAddressAsync(Guid id);
    Task AddAddressAsync(Address address);
    Task UpdateAddressAsync(Address address);
    Task DeleteAddressAsync(Address address);
}

