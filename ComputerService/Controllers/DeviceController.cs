using AutoMapper;
using ComputerService.Entities;
using ComputerService.Entities.Enums;
using ComputerService.Enums;
using ComputerService.Interfaces;
using ComputerService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ComputerService.Controllers;

[Route("api/v1/devices")]
[ApiVersion("1.0")]
[Authorize]
[ApiController]
public class DeviceController : BaseController<Device>
{
    private readonly IDeviceService _deviceService;
    public DeviceController(IDeviceService deviceService, IPaginationService paginationService, IMapper mapper, ILogger<BaseController<Device>> logger, IUserTrackingService userTrackingService) : base(paginationService, mapper, logger, userTrackingService)
    {
        _deviceService = deviceService;
    }

    [HttpGet]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<ActionResult<PagedListViewModel<PagedResponse<DeviceViewModel>>>> GetAllDevicesAsync([FromQuery] ParametersModel parameters, [FromQuery] DeviceSortEnum? sortOrder)
    {
        var devices = await _deviceService.GetPagedDevicesAsync(parameters, sortOrder);
        Logger.LogInformation("Returned {Count} devices from database. ", devices.Count());

        var mappedDevices = PaginationService.ToPagedListViewModelAsync<Device, DeviceViewModel>(devices);
        var pagedResponse = PaginationService.CreatePagedResponse(mappedDevices, parameters, sortOrder);

        return Ok(pagedResponse);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<ActionResult<Response<DeviceViewModel>>> GetDeviceAsync(Guid id)
    {
        var device = await _deviceService.GetDeviceAsync(id);
        CheckIfEntityExists(device, "Given device does not exist");
        return Ok(new Response<DeviceViewModel>(Mapper.Map<DeviceViewModel>(device)));
    }

    [HttpPost]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<IActionResult> AddDeviceAsync([FromBody] CreateDeviceModel createDeviceModel)
    {
        var device = Mapper.Map<Device>(createDeviceModel);
        await _deviceService.AddDeviceAsync(device);
        await UserTrackingService?.AddUserTrackingAsync(TrackingActionTypeEnum.CreateDevice, device.Id.ToString(), $"Added device '{device.Name}' to order with id '{device.OrderId}'")!;
        return Ok();
    }

    [HttpPatch("{id:guid}")]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<ActionResult> UpdateDevice(Guid id, [FromBody] JsonPatchDocument<UpdateDeviceModel> updateDeviceModelJpd)
    {
        var device = await _deviceService.GetDeviceAsync(id);
        CheckIfEntityExists(device, "Given device does not exist");
        await _deviceService.UpdateDeviceAsync(device, updateDeviceModelJpd);
        await UserTrackingService?.AddUserTrackingAsync(TrackingActionTypeEnum.UpdateDevice, device.Id.ToString(), $"Updated device '{device.Name}' in order with id '{device.OrderId}'")!;
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteDeviceAsync(Guid id)
    {
        var device = await _deviceService.GetDeviceAsync(id);
        CheckIfEntityExists(device, "Given device does not exist");
        await _deviceService.DeleteDeviceAsync(device);
        await UserTrackingService?.AddUserTrackingAsync(TrackingActionTypeEnum.DeleteDevice, device.Id.ToString(), $"Deleted device '{device.Name}' from order with id '{device.OrderId}'")!;
        return Ok();
    }
}