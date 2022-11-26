using AutoMapper;
using ComputerService.Data;
using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Interfaces;
using ComputerService.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using static System.String;

namespace ComputerService.Services;

public class AddressService : BaseEntityService<Address>, IAddressService
{
    public AddressService(ComputerServiceContext context, IValidator<Address> validator, IMapper mapper) : base(context, validator, mapper) { }


    public IQueryable<Address> GetAllAddressesAsync(ParametersModel parameters, AddressSortEnum? sortOrder)
    {
        var addresses = FindAll();
        if (sortOrder != null)
        {
            var asc = parameters.asc ?? true;
            addresses = sortOrder switch
            {
                AddressSortEnum.Country => asc
                    ? addresses.OrderBy(address => address.Country)
                    : addresses.OrderByDescending(address => address.Country),
                AddressSortEnum.State => asc
                    ? addresses.OrderBy(address => address.State)
                    : addresses.OrderByDescending(address => address.State),
                AddressSortEnum.City => asc
                    ? addresses.OrderBy(address => address.City)
                    : addresses.OrderByDescending(address => address.City),
                AddressSortEnum.PostalCode => asc
                    ? addresses.OrderBy(address => address.PostalCode)
                    : addresses.OrderByDescending(address => address.PostalCode),
                AddressSortEnum.Street => asc
                    ? addresses.OrderBy(address => address.Street)
                    : addresses.OrderByDescending(address => address.Street),
                AddressSortEnum.StreetNumber => asc
                    ? addresses.OrderBy(address => address.StreetNumber)
                    : addresses.OrderByDescending(address => address.StreetNumber),
                AddressSortEnum.Apartment => asc
                    ? addresses.OrderBy(address => address.Apartment)
                    : addresses.OrderByDescending(address => address.Apartment),
                _ => throw new ArgumentException()
            };
        }
        if (!IsNullOrEmpty(parameters.searchString))
        {
            addresses = addresses.Where(accessory => accessory.Country.Contains(parameters.searchString) ||
                                                            accessory.State.Contains(parameters.searchString) ||
                                                            accessory.City.Contains(parameters.searchString) ||
                                                            accessory.PostalCode.Contains(parameters.searchString) ||
                                                            accessory.Street.Contains(parameters.searchString) ||
                                                            accessory.StreetNumber.Contains(parameters.searchString) ||
                                                            accessory.Apartment.Contains(parameters.searchString));
        }
        return addresses;
    }

    public async Task<PagedList<Address>> GetPagedAddressesAsync(ParametersModel parameters, AddressSortEnum? sortOrder)
    {
        return await PagedList<Address>.ToPagedListAsync(GetAllAddressesAsync(parameters, sortOrder), parameters);
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