using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Models;

namespace ComputerService.Interfaces;

public interface IOrderDetailsService
{
    IQueryable<OrderDetails> GetAllOrderDetails(ParametersModel parameters, OrderDetailsSortEnum? sortOrder);
    Task<PagedList<OrderDetails>> GetPagedOrderDetailsAsync(ParametersModel parameters, OrderDetailsSortEnum? sortOrder);
    Task<OrderDetails> GetOrderDetailsAsync(Guid id);
    Task AddOrderDetailsAsync(OrderDetails orderDetails);
    Task UpdateOrderDetailsAsync(OrderDetails orderDetails);
    Task DeleteOrderDetailsAsync(OrderDetails orderDetails);
}