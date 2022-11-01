using AutoMapper;
using ComputerService.Data;
using ComputerService.Entities;
using ComputerService.Interfaces;
using ComputerService.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Services;
public class DeviceService : BaseEntityService<Device>, IDeviceService
{
    public DeviceService(ComputerServiceContext context, IValidator<Device> validator, IMapper mapper) : base(context, validator, mapper) { }

    public async Task<PagedList<Device>> GetAllDevicesAsync(ParametersModel parametersModel)
    {
        return await PagedList<Device>.ToPagedListAsync(FindAll(), parametersModel.PageNumber, parametersModel.PageSize);
    }

    public async Task<Device> GetDeviceAsync(Guid id)
    {
        return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddDeviceAsync(Device device)
    {
        await ValidateEntityAsync(device);
        await CreateAsync(device);
    }

    public async Task UpdateDeviceAsync(Device device)
    {
        await ValidateEntityAsync(device);
        await UpdateAsync(device);
    }

    public async Task DeleteDeviceAsync(Device device)
    {
        await DeleteAsync(device);
    }
}
