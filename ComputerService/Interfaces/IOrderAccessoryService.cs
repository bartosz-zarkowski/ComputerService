using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace ComputerService.Interfaces;

public interface IOrderAccessoryService
{
    IQueryable<OrderAccessory> GetAllOrderAccessories(ParametersModel parameters, OrderAccessorySortEnum? sortOrder);
    Task<PagedList<OrderAccessory>> GetPagedOrderAccessoriesAsync(ParametersModel parameters, OrderAccessorySortEnum? sortOrder);
    Task<OrderAccessory> GetOrderAccessoryAsync(Guid id);
    Task AddOrderAccessoryAsync(OrderAccessory orderAccessory);
    Task UpdateOrderAccessoryAsync(OrderAccessory orderAccessory, JsonPatchDocument<UpdateOrderAccessoryModel> updateOrderAccessoryModelJpd);
    Task DeleteOrderAccessoryAsync(OrderAccessory orderAccessory);
}