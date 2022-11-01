using ComputerService.Entities;
using ComputerService.Models;

namespace ComputerService.Interfaces;
public interface IOrderAccessoryService
{
    Task<PagedList<OrderAccessory>> GetAllOrderAccessoriesAsync(ParametersModel parameters);
    Task<OrderAccessory> GetOrderAccessoryAsync(Guid id);
    Task AddOrderAccessoryAsync(OrderAccessory orderAccessory);
    Task UpdateOrderAccessoryAsync(OrderAccessory orderAccessory);
    Task DeleteOrderAccessoryAsync(OrderAccessory orderAccessory);
}

