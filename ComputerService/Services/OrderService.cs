using AutoMapper;
using ComputerService.Data;
using ComputerService.Entities;
using ComputerService.Interfaces;
using ComputerService.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Services;
public class OrderService : BaseEntityService<Order>, IOrderService
{
    public OrderService(ComputerServiceContext context, IValidator<Order> validator, IMapper mapper) : base(context, validator, mapper) { }

    public async Task<PagedList<Order>> GetAllOrdersAsync(ParametersModel parametersModel)
    {
        return await PagedList<Order>.ToPagedListAsync(FindAll(), parametersModel.PageNumber, parametersModel.PageSize);
    }

    public async Task<Order> GetOrderAsync(Guid id)
    {
        return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddOrderAsync(Order Order)
    {
        await ValidateEntityAsync(Order);
        await CreateAsync(Order);
    }

    public async Task UpdateOrderAsync(Order Order)
    {
        await ValidateEntityAsync(Order);
        await UpdateAsync(Order);
    }

    public async Task DeleteOrderAsync(Order Order)
    {
        await DeleteAsync(Order);
    }
}
