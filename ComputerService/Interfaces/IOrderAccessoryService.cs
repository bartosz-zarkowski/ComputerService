using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Models;

namespace ComputerService.Interfaces;

public interface IOrderAccessoryService
{
    IQueryable<OrderAccessory> GetAllOrderAccessories(ParametersModel parameters, OrderAccessorySortEnum? sortOrder);
    Task<PagedList<OrderAccessory>> GetPagedOrderAccessoriesAsync(ParametersModel parameters, OrderAccessorySortEnum? sortOrder);
    Task<OrderAccessory> GetOrderAccessoryAsync(Guid id);
    Task AddOrderAccessoryAsync(OrderAccessory orderAccessory);
    Task UpdateOrderAccessoryAsync(OrderAccessory orderAccessory);
    Task DeleteOrderAccessoryAsync(OrderAccessory orderAccessory);
}