using AutoMapper;
using ComputerService.Entities;
using ComputerService.Interfaces;
using ComputerService.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComputerService.Controllers
{
    [Route("api/v1/accessorys")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AccessoryController : BaseController<Accessory>
    {
        private readonly IAccessoryService _accessoryService;
        public AccessoryController(IAccessoryService accessoryService, IPaginationService paginationService, IMapper mapper, ILogger<BaseController<Accessory>> logger) : base(paginationService, mapper, logger)
        {
            _accessoryService = accessoryService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedListViewModel<PagedResponse<AccessoryViewModel>>>> GetAllAccessoriesAsync([FromQuery] ParametersModel parameters)
        {
            var accessorys = await _accessoryService.GetAllAccessoriesAsync(parameters);
            Logger.LogInformation("Returned {Count} accessorys from database. ", accessorys.Count());

            var mappedAccessories = PaginationService.ToPagedListViewModelAsync<Accessory, AccessoryViewModel>(accessorys);
            var pagedResponse = PaginationService.CreatePagedResponse(mappedAccessories);

            return Ok(pagedResponse);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Response<AccessoryViewModel>>> GetAccessoryAsync(Guid id)
        {
            var accessory = await _accessoryService.GetAccessoryAsync(id);
            CheckIfEntityExists(accessory, "Given accessory does not exist");
            return Ok(new Response<AccessoryViewModel>(Mapper.Map<AccessoryViewModel>(accessory)));
        }

        [HttpPost]
        public async Task<IActionResult> AddAccessoryAsync([FromBody] CreateAccessoryModel createAccessoryModel)
        {
            var accessory = Mapper.Map<Accessory>(createAccessoryModel);
            await _accessoryService.AddAccessoryAsync(accessory);
            return Ok();
        }

        [HttpPatch("{id:guid}")]
        public async Task<ActionResult> UpdateAccessory(Guid id, [FromBody] UpdateAccessoryModel updateAccessoryModel)
        {
            var accessory = await _accessoryService.GetAccessoryAsync(id);
            CheckIfEntityExists(accessory, "Given accessory does not exist");
            var updatedAccessory = Mapper.Map(updateAccessoryModel, accessory);
            await _accessoryService.UpdateAccessoryAsync(updatedAccessory);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAccessoryAsync(Guid id)
        {
            var accessory = await _accessoryService.GetAccessoryAsync(id);
            CheckIfEntityExists(accessory, "Given accessory does not exist");
            await _accessoryService.DeleteAccessoryAsync(accessory);
            return Ok();
        }
    }
}
