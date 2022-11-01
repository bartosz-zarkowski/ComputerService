using ComputerService.Entities;
using ComputerService.Models;

namespace ComputerService.Interfaces;
public interface IDeviceService
{
    Task<PagedList<Device>> GetAllDevicesAsync(ParametersModel parameters);
    Task<Device> GetDeviceAsync(Guid id);
    Task AddDeviceAsync(Device device);
    Task UpdateDeviceAsync(Device device);
    Task DeleteDeviceAsync(Device device);
}

