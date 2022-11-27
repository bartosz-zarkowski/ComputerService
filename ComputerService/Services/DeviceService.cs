using AutoMapper;
using ComputerService.Data;
using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Interfaces;
using ComputerService.Models;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using static System.String;

namespace ComputerService.Services;

public class DeviceService : BaseEntityService<Device>, IDeviceService
{
    public DeviceService(ComputerServiceContext context, IValidator<Device> validator, IMapper mapper) : base(context, validator, mapper) { }

    public IQueryable<Device> GetAllDevices(ParametersModel parameters, DeviceSortEnum? sortOrder)
    {
        var devices = FindAll();
        if (sortOrder != null)
        {
            var asc = parameters.asc ?? true;
            devices = sortOrder switch
            {
                DeviceSortEnum.CreatedAt => asc
                    ? devices.OrderBy(device => device.CreatedAt)
                    : devices.OrderByDescending(device => device.CreatedAt),
                DeviceSortEnum.UpdatedAt => asc
                    ? devices.OrderBy(device => device.UpdatedAt)
                    : devices.OrderByDescending(device => device.UpdatedAt),
                DeviceSortEnum.Name => asc
                    ? devices.OrderBy(device => device.Name)
                    : devices.OrderByDescending(device => device.Name),
                DeviceSortEnum.SerialNumber => asc
                    ? devices.OrderBy(device => device.SerialNumber)
                    : devices.OrderByDescending(device => device.SerialNumber),
                DeviceSortEnum.Password => asc
                    ? devices.OrderBy(device => device.Password)
                    : devices.OrderByDescending(device => device.Password),
                DeviceSortEnum.Condition => asc
                    ? devices.OrderBy(device => device.Condition)
                    : devices.OrderByDescending(device => device.Condition),
                DeviceSortEnum.HasWarranty => asc
                    ? devices.OrderBy(device => device.HasWarranty)
                    : devices.OrderByDescending(device => device.HasWarranty),
                _ => throw new ArgumentException()
            };
        }
        if (!IsNullOrEmpty(parameters.searchString))
        {
            devices = devices.Where(device => device.Name.Contains(parameters.searchString) ||
                                              device.SerialNumber.Contains(parameters.searchString) ||
                                              device.Condition.Contains(parameters.searchString));
        }
        return devices;
    }

    public async Task<PagedList<Device>> GetPagedDevicesAsync(ParametersModel parameters, DeviceSortEnum? sortOrder)
    {
        return await PagedList<Device>.ToPagedListAsync(GetAllDevices(parameters, sortOrder), parameters);
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

    public async Task UpdateDeviceAsync(Device device, JsonPatchDocument<UpdateDeviceModel> updateDeviceModelJpd)
    {
        var mappedDevice = Mapper.Map<UpdateDeviceModel>(device);
        updateDeviceModelJpd.ApplyTo(mappedDevice);
        Mapper.Map(mappedDevice, device);
        await ValidateEntityAsync(device);
        await UpdateAsync(device);
    }

    public async Task DeleteDeviceAsync(Device device)
    {
        await DeleteAsync(device);
    }
}