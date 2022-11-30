using AutoMapper;
using ComputerService.Data;
using ComputerService.Entities;
using ComputerService.Entities.Enums;
using ComputerService.Enums;
using ComputerService.Interfaces;
using ComputerService.Models;
using ComputerService.Security;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using static System.String;

namespace ComputerService.Services;
public class OrderService : BaseEntityService<Order>, IOrderService
{
    private readonly ITokenManager _tokenManager;
    public OrderService(ComputerServiceContext context, IValidator<Order> validator, IMapper mapper, ITokenManager tokenManager) : base(context, validator, mapper)
    {
        _tokenManager = tokenManager;
    }

    public IQueryable<Order> GetAllOrders(ParametersModel parameters, OrderSortEnum? sortOrder)
    {
        var orders = FindAll();
        if (sortOrder != null)
        {
            var asc = parameters.asc ?? true;

            orders = sortOrder switch
            {
                OrderSortEnum.Title => asc
                    ? orders.OrderBy(order => order.Title)
                    : orders.OrderByDescending(order => order.Title),
                OrderSortEnum.CreatedAt => asc
                    ? orders.OrderBy(order => order.CreatedAt)
                    : orders.OrderByDescending(order => order.CreatedAt),
                OrderSortEnum.UpdatedAt => asc
                    ? orders.OrderBy(order => order.UpdatedAt)
                    : orders.OrderByDescending(order => order.UpdatedAt),
                OrderSortEnum.CompletedAt => asc
                    ? orders.OrderBy(order => order.CompletedAt)
                    : orders.OrderByDescending(order => order.CompletedAt),
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
                _ => throw new ArgumentException()
            };
        }
        if (!IsNullOrEmpty(parameters.searchString))
        {
            orders = orders.Where(order => order.Title.Contains(parameters.searchString) ||
                                           order.Description.Contains(parameters.searchString));
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

    public async Task AddOrderAsync(Order order)
    {
        order.CreatedBy = _tokenManager.GetCurrentUserId();
        await ValidateEntityAsync(order);
        await CreateAsync(order);
    }

    public async Task UpdateOrderAsync(Order order, JsonPatchDocument<UpdateOrderModel> updateOrderModelJpd)
    {
        var mappedOrder = Mapper.Map<UpdateOrderModel>(order);
        updateOrderModelJpd.ApplyTo(mappedOrder);
        Mapper.Map(mappedOrder, order);
        await ValidateEntityAsync(order);
        await UpdateAsync(order);
    }

    public async Task SetOrderAsCompleted(Order order, bool isCompleted)
    {
        order.CompletedBy = isCompleted ? _tokenManager.GetCurrentUserId() : null;
        order.CompletedAt = isCompleted ? DateTimeOffset.Now : null;
        order.Status = isCompleted ? OrderStatusEnum.Completed : OrderStatusEnum.ToCollect;
        await UpdateAsync(order);
    }

    public async Task DeleteOrderAsync(Order order)
    {
        await DeleteAsync(order);
    }
}