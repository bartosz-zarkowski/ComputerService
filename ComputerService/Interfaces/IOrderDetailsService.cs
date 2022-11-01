using ComputerService.Entities;
using ComputerService.Models;

namespace ComputerService.Interfaces;
public interface IOrderDetailsService
{
    Task<PagedList<OrderDetails>> GetAllOrderDetailsAsync(ParametersModel parameters);
    Task<OrderDetails> GetOrderDetailsAsync(Guid id);
    Task AddOrderDetailsAsync(OrderDetails orderDetails);
    Task UpdateOrderDetailsAsync(OrderDetails orderDetails);
    Task DeleteOrderDetailsAsync(OrderDetails orderDetails);
}

