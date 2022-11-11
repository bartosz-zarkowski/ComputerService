using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Models;

namespace ComputerService.Interfaces;

public interface IOrderService
{
    IQueryable<Order> GetAllOrders(ParametersModel parameters, OrderSortEnum? sortOrder);
    Task<PagedList<Order>> GetPagedOrdersAsync(ParametersModel parameters, OrderSortEnum? sortOrder);
    Task<Order> GetOrderAsync(Guid id);
    Task AddOrderAsync(Order order);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(Order order);
}