using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace ComputerService.Interfaces;

public interface IAccessoryService
{
    IQueryable<Accessory> GetAllAccessoriesAsync(ParametersModel parameters, AccessorySortEnum? sortOrder);
    Task<PagedList<Accessory>> GetPagedAccessoriesAsync(ParametersModel parameters, AccessorySortEnum? sortOrder);
    Task<Accessory> GetAccessoryAsync(Guid id);
    Task AddAccessoryAsync(Accessory accessory);
    Task UpdateAccessoryAsync(Accessory accessory, JsonPatchDocument<UpdateAccessoryModel> updateAccessoryModelJpd);
    Task DeleteAccessoryAsync(Accessory accessory);
}