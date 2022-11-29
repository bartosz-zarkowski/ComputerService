using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace ComputerService.Interfaces;

public interface IOrderService
{
    IQueryable<Order> GetAllOrders(ParametersModel parameters, OrderSortEnum? sortOrder);
    Task<PagedList<Order>> GetPagedOrdersAsync(ParametersModel parameters, OrderSortEnum? sortOrder);
    Task<Order> GetOrderAsync(Guid id);
    Task AddOrderAsync(Order order);
    Task UpdateOrderAsync(Order order, JsonPatchDocument<UpdateOrderModel> updateOrderModelJpd);
    Task SetOrderAsCompleted(Guid id, bool isCompleted);
    Task DeleteOrderAsync(Order order);
}