using AutoMapper;
using ComputerService.Data;
using ComputerService.Entities;
using ComputerService.Interfaces;
using ComputerService.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Services;
public class OrderDetailsService : BaseEntityService<OrderDetails>, IOrderDetailsService
{
    public OrderDetailsService(ComputerServiceContext context, IValidator<OrderDetails> validator, IMapper mapper) : base(context, validator, mapper) { }

    public async Task<PagedList<OrderDetails>> GetAllOrderDetailsAsync(ParametersModel parametersModel)
    {
        return await PagedList<OrderDetails>.ToPagedListAsync(FindAll(), parametersModel.PageNumber, parametersModel.PageSize);
    }

    public async Task<OrderDetails> GetOrderDetailsAsync(Guid id)
    {
        return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddOrderDetailsAsync(OrderDetails orderDetails)
    {
        await ValidateEntityAsync(orderDetails);
        await CreateAsync(orderDetails);
    }

    public async Task UpdateOrderDetailsAsync(OrderDetails orderDetails)
    {
        await ValidateEntityAsync(orderDetails);
        await UpdateAsync(orderDetails);
    }

    public async Task DeleteOrderDetailsAsync(OrderDetails orderDetails)
    {
        await DeleteAsync(orderDetails);
    }
}
