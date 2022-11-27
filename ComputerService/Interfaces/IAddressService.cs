using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace ComputerService.Interfaces;

public interface IAddressService
{
    IQueryable<Address> GetAllAddressesAsync(ParametersModel parameters, AddressSortEnum? sortOrder);
    Task<PagedList<Address>> GetPagedAddressesAsync(ParametersModel parameters, AddressSortEnum? sortOrder);
    Task<Address> GetAddressAsync(Guid id);
    Task AddAddressAsync(Address address);
    Task UpdateAddressAsync(Address address, JsonPatchDocument<UpdateAddressModel> updateAddressModelJpd);
    Task DeleteAddressAsync(Address address);
}