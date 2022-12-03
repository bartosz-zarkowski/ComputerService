using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace ComputerService.Interfaces;

public interface IDeviceService
{
    IQueryable<Device> GetAllDevices(ParametersModel parameters, DeviceSortEnum? sortOrder);
    Task<PagedList<Device>> GetPagedDevicesAsync(ParametersModel parameters, DeviceSortEnum? sortOrder);
    Task<Device> GetDeviceAsync(Guid id);
    Task AddDeviceAsync(Device device);
    Task UpdateDeviceAsync(Device device, JsonPatchDocument<UpdateDeviceModel> updateDeviceModelJpd);
    Task DeleteDeviceAsync(Device device);
}