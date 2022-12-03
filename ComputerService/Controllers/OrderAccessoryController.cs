using AutoMapper;
using ComputerService.Entities;
using ComputerService.Entities.Enums;
using ComputerService.Enums;
using ComputerService.Interfaces;
using ComputerService.Models;
using ComputerService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ComputerService.Controllers;

[Route("api/v1/orderAccessories")]
[ApiVersion("1.0")]
[Authorize]
[ApiController]
public class OrderAccessoryController : BaseController<OrderAccessory>
{
    private readonly IOrderAccessoryService _orderAccessoryService;
    public OrderAccessoryController(IOrderAccessoryService orderAccessoryService, IPaginationService paginationService, IMapper mapper, ILogger<BaseController<OrderAccessory>> logger, IUserTrackingService userTrackingService) : base(paginationService, mapper, logger, userTrackingService)
    {
        _orderAccessoryService = orderAccessoryService;
    }

    [HttpGet]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<ActionResult<PagedListViewModel<PagedResponse<OrderAccessoryViewModel>>>> GetAllOrderAccessoriesAsync([FromQuery] ParametersModel parameters, [FromQuery] OrderAccessorySortEnum? sortOrder)
    {
        var accessories = await _orderAccessoryService.GetPagedOrderAccessoriesAsync(parameters, sortOrder);
        Logger.LogInformation("Returned {Count} accessories from database. ", accessories.Count());

        var mappedOrderAccessories = PaginationService.ToPagedListViewModelAsync<OrderAccessory, OrderAccessoryViewModel>(accessories);
        var pagedResponse = PaginationService.CreatePagedResponse(mappedOrderAccessories, parameters, sortOrder);

        return Ok(pagedResponse);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<ActionResult<Response<OrderAccessoryViewModel>>> GetOrderAccessoryAsync(Guid id)
    {
        var orderAccessory = await _orderAccessoryService.GetOrderAccessoryAsync(id);
        CheckIfEntityExists(orderAccessory, "Given accessory does not exist");
        return Ok(new Response<OrderAccessoryViewModel>(Mapper.Map<OrderAccessoryViewModel>(orderAccessory)));
    }

    [HttpPost]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<IActionResult> AddOrderAccessoryAsync([FromBody] CreateOrderAccessoryModel createOrderAccessoryModel)
    {
        var orderAccessory = Mapper.Map<OrderAccessory>(createOrderAccessoryModel);
        await _orderAccessoryService.AddOrderAccessoryAsync(orderAccessory);
        await UserTrackingService?.AddUserTrackingAsync(TrackingActionTypeEnum.CreateOrderAccessory, orderAccessory.Id.ToString()
            , $"Added accessory '{orderAccessory.Name}' to order with id '{orderAccessory.OrderId}'")!;
        return Ok(new { orderAccessoryId = orderAccessory.Id });
    }

    [HttpPatch("{id:guid}")]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<ActionResult> UpdateOrderAccessory(Guid id, [FromBody] JsonPatchDocument<UpdateOrderAccessoryModel> updateOrderAccessoryModelJpd)
    {
        var orderAccessory = await _orderAccessoryService.GetOrderAccessoryAsync(id);
        CheckIfEntityExists(orderAccessory, "Given orderAccessory does not exist");
        await _orderAccessoryService.UpdateOrderAccessoryAsync(orderAccessory, updateOrderAccessoryModelJpd);
        await UserTrackingService?.AddUserTrackingAsync(TrackingActionTypeEnum.UpdateOrderAccessory, orderAccessory.Id.ToString()
            , $"Updated accessory '{orderAccessory.Name}' in order with id '{orderAccessory.OrderId}'")!;
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<IActionResult> DeleteOrderAccessoryAsync(Guid id)
    {
        var orderAccessory = await _orderAccessoryService.GetOrderAccessoryAsync(id);
        CheckIfEntityExists(orderAccessory, "Given orderAccessory does not exist");
        await _orderAccessoryService.DeleteOrderAccessoryAsync(orderAccessory);
        await UserTrackingService?.AddUserTrackingAsync(TrackingActionTypeEnum.DeleteOrderAccessory, orderAccessory.Id.ToString()
            , $"Deleted accessory '{orderAccessory.Name}' from order with id '{orderAccessory.OrderId}'")!;
        return Ok();
    }
}