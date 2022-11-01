using AutoMapper;
using ComputerService.Data;
using ComputerService.Entities;
using ComputerService.Interfaces;
using ComputerService.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Services;
public class OrderAccessoryService : BaseEntityService<OrderAccessory>, IOrderAccessoryService
{
    public OrderAccessoryService(ComputerServiceContext context, IValidator<OrderAccessory> validator, IMapper mapper) : base(context, validator, mapper) { }

    public async Task<PagedList<OrderAccessory>> GetAllOrderAccessoriesAsync(ParametersModel parametersModel)
    {
        return await PagedList<OrderAccessory>.ToPagedListAsync(FindAll(), parametersModel.PageNumber, parametersModel.PageSize);
    }

    public async Task<OrderAccessory> GetOrderAccessoryAsync(Guid id)
    {
        return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddOrderAccessoryAsync(OrderAccessory orderAccessory)
    {
        await ValidateEntityAsync(orderAccessory);
        await CreateAsync(orderAccessory);
    }

    public async Task UpdateOrderAccessoryAsync(OrderAccessory orderAccessory)
    {
        await ValidateEntityAsync(orderAccessory);
        await UpdateAsync(orderAccessory);
    }

    public async Task DeleteOrderAccessoryAsync(OrderAccessory orderAccessory)
    {
        await DeleteAsync(orderAccessory);
    }
}
