using AutoMapper;
using ComputerService.Data;
using ComputerService.Entities;
using ComputerService.Interfaces;
using ComputerService.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Services;
public class AddressService : BaseEntityService<Address>, IAddressService
{
    public AddressService(ComputerServiceContext context, IValidator<Address> validator, IMapper mapper) : base(context, validator, mapper) { }

    public async Task<PagedList<Address>> GetAllAddressesAsync(ParametersModel parametersModel)
    {
        return await PagedList<Address>.ToPagedListAsync(FindAll(), parametersModel.PageNumber, parametersModel.PageSize);
    }

    public async Task<Address> GetAddressAsync(Guid id)
    {
        return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddAddressAsync(Address address)
    {
        await ValidateEntityAsync(address);
        await CreateAsync(address);
    }

    public async Task UpdateAddressAsync(Address address)
    {
        await ValidateEntityAsync(address);
        await UpdateAsync(address);
    }

    public async Task DeleteAddressAsync(Address address)
    {
        await DeleteAsync(address);
    }
}
