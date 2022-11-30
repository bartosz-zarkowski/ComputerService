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

[Route("api/v1/orders")]
[ApiVersion("1.0")]
[Authorize]
[ApiController]
public class OrderController : BaseController<Order>
{
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService, IPaginationService paginationService, IMapper mapper, ILogger<BaseController<Order>> logger, IUserTrackingService userTrackingService) : base(paginationService, mapper, logger, userTrackingService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<ActionResult<PagedListViewModel<PagedResponse<OrderViewModel>>>> GetAllOrdersAsync([FromQuery] ParametersModel parameters, [FromQuery] OrderSortEnum? sortOrder)
    {
        var orders = await _orderService.GetPagedOrdersAsync(parameters, sortOrder);
        Logger.LogInformation("Returned {Count} orders from database. ", orders.Count());

        var mappedOrders = PaginationService.ToPagedListViewModelAsync<Order, OrderViewModel>(orders);
        var pagedResponse = PaginationService.CreatePagedResponse(mappedOrders, parameters, sortOrder);

        return Ok(pagedResponse);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<ActionResult<Response<OrderViewModel>>> GetOrderAsync(Guid id)
    {
        var order = await _orderService.GetOrderAsync(id);
        CheckIfEntityExists(order, "Given order does not exist");
        return Ok(new Response<OrderViewModel>(Mapper.Map<OrderViewModel>(order)));
    }

    [HttpPost]
    [Authorize(Roles = "Administrator, Receiver")]
    public async Task<IActionResult> AddOrderAsync([FromBody] CreateOrderModel createOrderModel)
    {
        var order = Mapper.Map<Order>(createOrderModel);
        await _orderService.AddOrderAsync(order);
        await UserTrackingService?.AddUserTrackingAsync(TrackingActionTypeEnum.CreateOrder, order.Id.ToString()
            , $"Created order '{order.Title}' for customer with id '{order.CustomerId}'")!;
        return Ok();
    }

    [HttpPatch("{id:guid}")]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<ActionResult> UpdateOrder(Guid id, [FromBody] JsonPatchDocument<UpdateOrderModel> updateOrderModelJpd)
    {
        var order = await _orderService.GetOrderAsync(id);
        CheckIfEntityExists(order, "Given order does not exist");
        await _orderService.UpdateOrderAsync(order, updateOrderModelJpd);
        await UserTrackingService?.AddUserTrackingAsync(TrackingActionTypeEnum.UpdateOrder, order.Id.ToString()
            , $"Updated order '{order.Title}' of customer with id '{order.CustomerId}'")!;
        return Ok();
    }

    [HttpPatch("/complete/{id:guid}")]
    [Authorize(Roles = "Administrator, Receiver")]
    public async Task<ActionResult> SetOrderAsCompleted(Guid id, [FromQuery] bool? isCompleted)
    {
        var order = await _orderService.GetOrderAsync(id);
        await _orderService.SetOrderAsCompleted(order, isCompleted ?? true);
        await UserTrackingService?.AddUserTrackingAsync(TrackingActionTypeEnum.SetOrderAsCompleted, order.Id.ToString()
            , $"Completed order '{order.Title}' of customer with id '{order.CustomerId}'")!;
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteOrderAsync(Guid id)
    {
        var order = await _orderService.GetOrderAsync(id);
        CheckIfEntityExists(order, "Given order does not exist");
        await _orderService.DeleteOrderAsync(order);
        await UserTrackingService?.AddUserTrackingAsync(TrackingActionTypeEnum.DeleteOrder, order.Id.ToString()
            , $"Deleted order '{order.Title}' of customer with id '{order.CustomerId}'")!;
        return Ok();
    }
}