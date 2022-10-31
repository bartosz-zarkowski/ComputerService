using AutoMapper;
using ComputerService.Entities;
using ComputerService.Interfaces;
using ComputerService.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComputerService.Controllers
{
    [Route("api/v1/devices")]
    [ApiVersion("1.0")]
    [ApiController]
    public class DeviceController : BaseController<Device>
    {
        private readonly IDeviceService _deviceService;
        public DeviceController(IDeviceService deviceService, IPaginationService paginationService, IMapper mapper, ILogger<BaseController<Device>> logger) : base(paginationService, mapper, logger)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedListViewModel<PagedResponse<DeviceViewModel>>>> GetAllOrdersAsync([FromQuery] ParametersModel parameters)
        {
            var devices = await _deviceService.GetAllDevicesAsync(parameters);
            Logger.LogInformation("Returned {Count} devices from database. ", devices.Count());

            var mappedDevices = PaginationService.ToPagedListViewModelAsync<Device, DeviceViewModel>(devices);
            var pagedResponse = PaginationService.CreatePagedResponse(mappedDevices);

            return Ok(pagedResponse);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Response<DeviceViewModel>>> GetDeviceAsync(Guid id)
        {
            var device = await _deviceService.GetDeviceAsync(id);
            CheckIfEntityExists(device, "Given device does not exist");
            return Ok(new Response<DeviceViewModel>(Mapper.Map<DeviceViewModel>(device)));
        }

        [HttpPost]
        public async Task<IActionResult> AddDeviceAsync([FromBody] CreateDeviceModel createDeviceModel)
        {
            var device = Mapper.Map<Device>(createDeviceModel);
            await _deviceService.AddDeviceAsync(device);
            return Ok();
        }

        [HttpPatch("{id:guid}")]
        public async Task<ActionResult> UpdateDevice(Guid id, [FromBody] UpdateDeviceModel updateDeviceModel)
        {
            var device = await _deviceService.GetDeviceAsync(id);
            CheckIfEntityExists(device, "Given device does not exist");
            var updatedDevice = Mapper.Map(updateDeviceModel, device);
            await _deviceService.UpdateDeviceAsync(updatedDevice);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteDeviceAsync(Guid id)
        {
            var device = await _deviceService.GetDeviceAsync(id);
            CheckIfEntityExists(device, "Given device does not exist");
            await _deviceService.DeleteDeviceAsync(device);
            return Ok();
        }
    }
}
