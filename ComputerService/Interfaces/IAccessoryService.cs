using ComputerService.Entities;
using ComputerService.Models;

namespace ComputerService.Interfaces;
public interface IAccessoryService
{
    Task<PagedList<Accessory>> GetAllAccessoriesAsync(ParametersModel parameters);
    Task<Accessory> GetAccessoryAsync(Guid id);
    Task AddAccessoryAsync(Accessory accessory);
    Task UpdateAccessoryAsync(Accessory accessory);
    Task DeleteAccessoryAsync(Accessory accessory);
}

