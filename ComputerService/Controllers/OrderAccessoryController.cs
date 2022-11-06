using AutoMapper;
using ComputerService.Entities;
using ComputerService.Interfaces;
using ComputerService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerService.Controllers;

[Route("api/v1/orderAccessories")]
[ApiVersion("1.0")]
[Authorize]
[ApiController]
public class OrderAccessoryController : BaseController<OrderAccessory>
{
    private readonly IOrderAccessoryService _orderAccessoryService;
    public OrderAccessoryController(IOrderAccessoryService orderAccessoryService, IPaginationService paginationService, IMapper mapper, ILogger<BaseController<OrderAccessory>> logger) : base(paginationService, mapper, logger)
    {
        _orderAccessoryService = orderAccessoryService;
    }

    [HttpGet]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<ActionResult<PagedListViewModel<PagedResponse<OrderAccessoryViewModel>>>> GetAllOrderAccessoriesAsync([FromQuery] ParametersModel parameters)
    {
        var accessories = await _orderAccessoryService.GetAllOrderAccessoriesAsync(parameters);
        Logger.LogInformation("Returned {Count} accessories from database. ", accessories.Count());

        var mappedOrderAccessories = PaginationService.ToPagedListViewModelAsync<OrderAccessory, OrderAccessoryViewModel>(accessories);
        var pagedResponse = PaginationService.CreatePagedResponse(mappedOrderAccessories);

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
        return Ok();
    }

    [HttpPatch("{id:guid}")]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<ActionResult> UpdateOrderAccessory(Guid id, [FromBody] UpdateOrderAccessoryModel updateOrderAccessoryModel)
    {
        var orderAccessory = await _orderAccessoryService.GetOrderAccessoryAsync(id);
        CheckIfEntityExists(orderAccessory, "Given orderAccessory does not exist");
        var updatedOrderAccessory = Mapper.Map(updateOrderAccessoryModel, orderAccessory);
        await _orderAccessoryService.UpdateOrderAccessoryAsync(updatedOrderAccessory);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<IActionResult> DeleteOrderAccessoryAsync(Guid id)
    {
        var orderAccessory = await _orderAccessoryService.GetOrderAccessoryAsync(id);
        CheckIfEntityExists(orderAccessory, "Given orderAccessory does not exist");
        await _orderAccessoryService.DeleteOrderAccessoryAsync(orderAccessory);
        return Ok();
    }
}