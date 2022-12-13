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

[Route("api/v1/customers")]
[ApiVersion("1.0")]
[Authorize]
[ApiController]
public class CustomerController : BaseController<Customer>
{
    private readonly ICustomerService _customerService;
    public CustomerController(ICustomerService customerService, IPaginationService paginationService, IMapper mapper, ILogger<BaseController<Customer>> logger, IUserTrackingService userTrackingService) : base(paginationService, mapper, logger, userTrackingService)
    {
        _customerService = customerService;
    }

    [Authorize(Roles = "Administrator, Receiver, Technician")]
    [HttpGet]
    public async Task<ActionResult<PagedListViewModel<PagedResponse<CustomerViewModel>>>> GetAllCustomersAsync([FromQuery] ParametersModel parameters, [FromQuery] CustomerSortEnum? sortOrder)
    {
        var customers = await _customerService.GetPagedCustomersAsync(parameters, sortOrder);
        Logger.LogInformation("Returned {Count} customers from database. ", customers.Count());

        var mappedCustomers = PaginationService.ToPagedListViewModelAsync<Customer, CustomerViewModel>(customers);
        var pagedResponse = PaginationService.CreatePagedResponse(mappedCustomers, parameters, sortOrder);

        return Ok(pagedResponse);
    }

    [Authorize(Roles = "Administrator, Receiver, Technician")]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Response<CustomerViewModel>>> GetCustomerAsync(Guid id)
    {
        var customer = await _customerService.GetCustomerAsync(id);
        CheckIfEntityExists(customer, "Given customer does not exist");
        return Ok(new Response<CustomerViewModel>(Mapper.Map<CustomerViewModel>(customer)));
    }

    [Authorize(Roles = "Administrator, Receiver")]
    [HttpPost]
    public async Task<IActionResult> AddCustomerAsync([FromBody] CreateCustomerModel createCustomerModel)
    {
        var customer = Mapper.Map<Customer>(createCustomerModel);
        await _customerService.AddCustomerAsync(customer);
        await UserTrackingService?.AddUserTrackingAsync(TrackingActionTypeEnum.CreateCustomer, customer.Id.ToString(), $"Created customer '{customer.FirstName} {customer.LastName}'")!;
        return Ok(new { customerId = customer.Id });
    }

    [Authorize(Roles = "Administrator, Receiver, Technician")]
    [HttpPatch("{id:guid}")]
    public async Task<ActionResult> UpdateCustomer(Guid id, [FromBody] JsonPatchDocument<UpdateCustomerModel> updateCustomerModelJpd)
    {
        var customer = await _customerService.GetCustomerAsync(id);
        CheckIfEntityExists(customer, "Given customer does not exist");
        await _customerService.UpdateCustomerAsync(customer, updateCustomerModelJpd);
        await UserTrackingService?.AddUserTrackingAsync(TrackingActionTypeEnum.UpdateCustomer, customer.Id.ToString(), $"Updated customer '{customer.FirstName} {customer.LastName}'")!;
        return Ok();
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCustomerAsync(Guid id)
    {
        var customer = await _customerService.GetCustomerAsync(id);
        CheckIfEntityExists(customer, "Given customer does not exist");
        await _customerService.DeleteCustomerAsync(customer);
        await UserTrackingService?.AddUserTrackingAsync(TrackingActionTypeEnum.DeleteCustomer, customer.Id.ToString(), $"Deleted customer '{customer.FirstName} {customer.LastName}'")!;
        return Ok();
    }
}