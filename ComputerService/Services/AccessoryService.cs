using AutoMapper;
using ComputerService.Data;
using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Interfaces;
using ComputerService.Models;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using static System.String;

namespace ComputerService.Services;
public class AccessoryService : BaseEntityService<Accessory>, IAccessoryService
{
    public AccessoryService(ComputerServiceContext context, IValidator<Accessory> validator, IMapper mapper) : base(context, validator, mapper) { }

    public IQueryable<Accessory> GetAllAccessoriesAsync(ParametersModel parameters, AccessorySortEnum? sortOrder)
    {
        var accessories = FindAll();
        if (sortOrder != null)
        {
            var asc = parameters.asc ?? true;
            accessories = sortOrder switch
            {
                AccessorySortEnum.Name => asc
                    ? accessories.OrderBy(accessory => accessory.Name)
                    : accessories.OrderByDescending(accessory => accessory.Name),
                _ => throw new ArgumentException()
            };
        }

        if (!IsNullOrEmpty(parameters.searchString))
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

    public async Task UpdateAccessoryAsync(Accessory accessory, JsonPatchDocument<UpdateAccessoryModel> updateAccessoryModelJpd)
    {
        var mappedAccessory = Mapper.Map<UpdateAccessoryModel>(accessory);
        updateAccessoryModelJpd.ApplyTo(mappedAccessory);
        Mapper.Map(mappedAccessory, accessory);
        await ValidateEntityAsync(accessory);
        await UpdateAsync(accessory);
    }

    public async Task DeleteAccessoryAsync(Accessory accessory)
    {
        await DeleteAsync(accessory);
    }
}