using ComputerService.Entities;
using ComputerService.Entities.Enums;
using ComputerService.Enums;
using ComputerService.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace ComputerService.Interfaces;

public interface IOrderService
{
    IQueryable<Order> GetAllOrders(ParametersModel parameters, OrderSortEnum? sortOrder, OrderStatusEnum? orderStatus);
    Task<PagedList<Order>> GetPagedOrdersAsync(ParametersModel parameters, OrderSortEnum? sortOrder, OrderStatusEnum? orderStatus);
    Task<Order> GetOrderAsync(Guid id);
    Task AddOrderAsync(Order order);
    Task UpdateOrderAsync(Order order, JsonPatchDocument<UpdateOrderModel> updateOrderModelJpd);
    Task SetOrderAsServiced(Order order, bool isServiced);
    Task SetOrderAsCompleted(Order order, bool isCompleted);
    Task DeleteOrderAsync(Order order);
}