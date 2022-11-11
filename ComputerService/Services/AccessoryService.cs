using AutoMapper;
using Castle.Core.Internal;
using ComputerService.Data;
using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Interfaces;
using ComputerService.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Services;
public class AccessoryService : BaseEntityService<Accessory>, IAccessoryService
{
    public AccessoryService(ComputerServiceContext context, IValidator<Accessory> validator, IMapper mapper) : base(context, validator, mapper) { }

    public IQueryable<Accessory> GetAllAccessoriesAsync(ParametersModel parameters, AccessorySortEnum? sortOrder)
    {
        var accessories = FindAll();
        if (sortOrder != null)
        {
            bool asc = (bool)parameters.asc;
            accessories = Enum.IsDefined(typeof(AccessorySortEnum), sortOrder)
                ? sortOrder switch
                {
                    AccessorySortEnum.Name => asc
                        ? accessories.OrderBy(accessory => accessory.Name)
                        : accessories.OrderByDescending(accessory => accessory.Name),
                }
                : throw new ArgumentException();
        }

        if (!parameters.searchString.IsNullOrEmpty())
        {
            accessories = accessories.Where(accessory => accessory.Name.Contains(parameters.searchString));
        }

        return accessories;
    }

    public async Task<PagedList<Accessory>> GetPagedAccessoriesAsync(ParametersModel parameters, AccessorySortEnum? sortOrder)
    {
        return await PagedList<Accessory>.ToPagedListAsync(GetAllAccessoriesAsync(parameters, sortOrder), parameters);
    }

    public async Task<Accessory> GetAccessoryAsync(Guid id)
    {
        return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddAccessoryAsync(Accessory accessory)
    {
        await ValidateEntityAsync(accessory);
        await CreateAsync(accessory);
    }

    public async Task UpdateAccessoryAsync(Accessory accessory)
    {
        await ValidateEntityAsync(accessory);
        await UpdateAsync(accessory);
    }

    public async Task DeleteAccessoryAsync(Accessory accessory)
    {
        await DeleteAsync(accessory);
    }
}