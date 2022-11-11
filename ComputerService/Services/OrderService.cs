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
public class OrderService : BaseEntityService<Order>, IOrderService
{
    public OrderService(ComputerServiceContext context, IValidator<Order> validator, IMapper mapper) : base(context, validator, mapper) { }

    public IQueryable<Order> GetAllOrders(ParametersModel parameters, OrderSortEnum? sortOrder)
    {
        var orders = FindAll();
        if (sortOrder != null)
        {
            bool asc = (bool)parameters.asc;
            orders = Enum.IsDefined(typeof(OrderSortEnum), sortOrder)
                ? sortOrder switch
                {
                    OrderSortEnum.CreatedAt => asc
                        ? orders.OrderBy(order => order.CreatedAt)
                        : orders.OrderByDescending(order => order.CreatedAt),
                    OrderSortEnum.UpdatedAt => asc
                        ? orders.OrderBy(order => order.UpdatedAt)
                        : orders.OrderByDescending(order => order.UpdatedAt),
                    OrderSortEnum.ReceivedAt => asc
                        ? orders.OrderBy(order => order.ReceivedAt)
                        : orders.OrderByDescending(order => order.ReceivedAt),
                    OrderSortEnum.Status => asc
                        ? orders.OrderBy(order => order.Status)
                        : orders.OrderByDescending(order => order.Status),
                    OrderSortEnum.CreatedBy => asc
                        ? orders.OrderBy(order => order.CreatedBy)
                        : orders.OrderByDescending(order => order.CreatedBy),
                    OrderSortEnum.ServicedBy => asc
                        ? orders.OrderBy(order => order.ServicedBy)
                        : orders.OrderByDescending(order => order.ServicedBy),
                    OrderSortEnum.CompletedBy => asc
                        ? orders.OrderBy(order => order.CompletedBy)
                        : orders.OrderByDescending(order => order.CompletedBy),
                }
                : throw new ArgumentException();
        }
        if (!parameters.searchString.IsNullOrEmpty())
        {
            orders = orders.Where(order => order.Description.Contains(parameters.searchString));
        }
        return orders;
    }

    public async Task<PagedList<Order>> GetPagedOrdersAsync(ParametersModel parameters, OrderSortEnum? sortOrder)
    {
        return await PagedList<Order>.ToPagedListAsync(GetAllOrders(parameters, sortOrder), parameters);
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