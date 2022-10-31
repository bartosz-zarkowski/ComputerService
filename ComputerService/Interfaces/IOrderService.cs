using ComputerService.Entities;
using ComputerService.Models;

namespace ComputerService.Interfaces;
public interface IOrderService
{
    Task<PagedList<Order>> GetAllOrdersAsync(ParametersModel parameters);
    Task<Order> GetOrderAsync(Guid id);
    Task AddOrderAsync(Order order);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(Order order);
}

