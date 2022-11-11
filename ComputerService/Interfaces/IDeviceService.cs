using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Models;

namespace ComputerService.Interfaces;

public interface IDeviceService
{
    IQueryable<Device> GetAllDevices(ParametersModel parameters, DeviceSortEnum? sortOrder);
    Task<PagedList<Device>> GetPagedDevicesAsync(ParametersModel parameters, DeviceSortEnum? sortOrder);
    Task<Device> GetDeviceAsync(Guid id);
    Task AddDeviceAsync(Device device);
    Task UpdateDeviceAsync(Device device);
    Task DeleteDeviceAsync(Device device);
}