using AutoMapper;
using ComputerService.Entities;
using ComputerService.Interfaces;
using ComputerService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerService.Controllers;
[Route("api/v1/orderDetails")]
[ApiVersion("1.0")]
[Authorize]
[ApiController]
public class OrderDetailsController : BaseController<OrderDetails>
{
    private readonly IOrderDetailsService _orderDetailsService;
    public OrderDetailsController(IOrderDetailsService orderDetailsService, IPaginationService paginationService, IMapper mapper, ILogger<BaseController<OrderDetails>> logger) : base(paginationService, mapper, logger)
    {
        _orderDetailsService = orderDetailsService;
    }

    [HttpGet]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<ActionResult<PagedListViewModel<PagedResponse<OrderDetailsViewModel>>>> GetAllOrdersAsync([FromQuery] ParametersModel parameters)
    {
        var orderDetails = await _orderDetailsService.GetAllOrderDetailsAsync(parameters);
        Logger.LogInformation("Returned {Count} orderDetails from database. ", orderDetails.Count());

        var mappedOrderDetails = PaginationService.ToPagedListViewModelAsync<OrderDetails, OrderDetailsViewModel>(orderDetails);
        var pagedResponse = PaginationService.CreatePagedResponse(mappedOrderDetails);

        return Ok(pagedResponse);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<ActionResult<Response<OrderDetailsViewModel>>> GetOrderDetailsAsync(Guid id)
    {
        var orderDetails = await _orderDetailsService.GetOrderDetailsAsync(id);
        CheckIfEntityExists(orderDetails, "Given orderDetails does not exist");
        return Ok(new Response<OrderDetailsViewModel>(Mapper.Map<OrderDetailsViewModel>(orderDetails)));
    }

    [HttpPost]
    [Authorize(Roles = "Administrator, Receiver")]
    public async Task<IActionResult> AddOrderDetailsAsync([FromBody] CreateOrderDetailsModel createOrderDetailsModel)
    {
        var orderDetails = Mapper.Map<OrderDetails>(createOrderDetailsModel);
        await _orderDetailsService.AddOrderDetailsAsync(orderDetails);
        return Ok();
    }

    [HttpPatch("{id:guid}")]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<ActionResult> UpdateOrderDetails(Guid id, [FromBody] UpdateOrderDetailsModel updateOrderDetailsModel)
    {
        var orderDetails = await _orderDetailsService.GetOrderDetailsAsync(id);
        CheckIfEntityExists(orderDetails, "Given orderDetails does not exist");
        var updatedOrderDetails = Mapper.Map(updateOrderDetailsModel, orderDetails);
        await _orderDetailsService.UpdateOrderDetailsAsync(updatedOrderDetails);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteOrderDetailsAsync(Guid id)
    {
        var orderDetails = await _orderDetailsService.GetOrderDetailsAsync(id);
        CheckIfEntityExists(orderDetails, "Given orderDetails does not exist");
        await _orderDetailsService.DeleteOrderDetailsAsync(orderDetails);
        return Ok();
    }
}