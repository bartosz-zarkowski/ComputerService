using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Models;

namespace ComputerService.Interfaces;

public interface IAccessoryService
{
    IQueryable<Accessory> GetAllAccessoriesAsync(ParametersModel parameters, AccessorySortEnum? sortOrder);
    Task<PagedList<Accessory>> GetPagedAccessoriesAsync(ParametersModel parameters, AccessorySortEnum? sortOrder);
    Task<Accessory> GetAccessoryAsync(Guid id);
    Task AddAccessoryAsync(Accessory accessory);
    Task UpdateAccessoryAsync(Accessory accessory);
    Task DeleteAccessoryAsync(Accessory accessory);
}