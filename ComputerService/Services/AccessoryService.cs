using AutoMapper;
using ComputerService.Data;
using ComputerService.Entities;
using ComputerService.Interfaces;
using ComputerService.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Services;
public class AccessoryService : BaseEntityService<Accessory>, IAccessoryService
{
    public AccessoryService(ComputerServiceContext context, IValidator<Accessory> validator, IMapper mapper) : base(context, validator, mapper) { }

    public async Task<PagedList<Accessory>> GetAllAccessoriesAsync(ParametersModel parametersModel)
    {
        return await PagedList<Accessory>.ToPagedListAsync(FindAll(), parametersModel.PageNumber, parametersModel.PageSize);
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
