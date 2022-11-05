using AutoMapper;
using ComputerService.Entities;
using ComputerService.Interfaces;
using ComputerService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerService.Controllers;

[Route("api/v1/orders")]
[ApiVersion("1.0")]
[Authorize]
[ApiController]
public class OrderController : BaseController<Order>
{
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService, IPaginationService paginationService, IMapper mapper, ILogger<BaseController<Order>> logger) : base(paginationService, mapper, logger)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedListViewModel<PagedResponse<OrderViewModel>>>> GetAllOrdersAsync([FromQuery] ParametersModel parameters)
    {
        var orders = await _orderService.GetAllOrdersAsync(parameters);
        Logger.LogInformation("Returned {Count} orders from database. ", orders.Count());

        var mappedOrders = PaginationService.ToPagedListViewModelAsync<Order, OrderViewModel>(orders);
        var pagedResponse = PaginationService.CreatePagedResponse(mappedOrders);

        return Ok(pagedResponse);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Response<OrderViewModel>>> GetOrderAsync(Guid id)
    {
        var order = await _orderService.GetOrderAsync(id);
        CheckIfEntityExists(order, "Given order does not exist");
        return Ok(new Response<OrderViewModel>(Mapper.Map<OrderViewModel>(order)));
    }

    [HttpPost]
    public async Task<IActionResult> AddOrderAsync([FromBody] CreateOrderModel createOrderModel)
    {
        var order = Mapper.Map<Order>(createOrderModel);
        await _orderService.AddOrderAsync(order);
        return Ok();
    }

    [HttpPatch("{id:guid}")]
    public async Task<ActionResult> UpdateOrder(Guid id, [FromBody] UpdateOrderModel updateOrderModel)
    {
        var order = await _orderService.GetOrderAsync(id);
        CheckIfEntityExists(order, "Given order does not exist");
        var updatedOrder = Mapper.Map(updateOrderModel, order);
        await _orderService.UpdateOrderAsync(updatedOrder);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteOrderAsync(Guid id)
    {
        var order = await _orderService.GetOrderAsync(id);
        CheckIfEntityExists(order, "Given order does not exist");
        await _orderService.DeleteOrderAsync(order);
        return Ok();
    }
}