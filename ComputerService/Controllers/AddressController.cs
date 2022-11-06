using AutoMapper;
using ComputerService.Entities;
using ComputerService.Interfaces;
using ComputerService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerService.Controllers;

[Route("api/v1/addresses")]
[ApiVersion("1.0")]
[Authorize]
[ApiController]
public class AddressController : BaseController<Address>
{
    private readonly IAddressService _addressService;
    public AddressController(IAddressService addressService, IPaginationService paginationService, IMapper mapper, ILogger<BaseController<Address>> logger) : base(paginationService, mapper, logger)
    {
        _addressService = addressService;
    }

    [HttpGet]
    [Authorize(Roles = "Administrator, Receiver")]
    public async Task<ActionResult<PagedListViewModel<PagedResponse<AddressViewModel>>>> GetAllAddressesAsync([FromQuery] ParametersModel parameters)
    {
        var addresses = await _addressService.GetAllAddressesAsync(parameters);
        Logger.LogInformation("Returned {Count} addresses from database. ", addresses.Count());

        var mappedAddresses = PaginationService.ToPagedListViewModelAsync<Address, AddressViewModel>(addresses);
        var pagedResponse = PaginationService.CreatePagedResponse(mappedAddresses);

        return Ok(pagedResponse);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<ActionResult<Response<AddressViewModel>>> GetAddressAsync(Guid id)
    {
        var address = await _addressService.GetAddressAsync(id);
        CheckIfEntityExists(address, "Given address does not exist");
        return Ok(new Response<AddressViewModel>(Mapper.Map<AddressViewModel>(address)));
    }

    [HttpPost]
    [Authorize(Roles = "Administrator, Receiver")]
    public async Task<IActionResult> AddAddressAsync([FromBody] CreateAddressModel createAddressModel)
    {
        var address = Mapper.Map<Address>(createAddressModel);
        await _addressService.AddAddressAsync(address);
        return Ok();
    }

    [HttpPatch("{id:guid}")]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<ActionResult> UpdateAddress(Guid id, [FromBody] UpdateAddressModel updateAddressModel)
    {
        var address = await _addressService.GetAddressAsync(id);
        CheckIfEntityExists(address, "Given address does not exist");
        var updatedAddress = Mapper.Map(updateAddressModel, address);
        await _addressService.UpdateAddressAsync(updatedAddress);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator, Receiver")]
    public async Task<IActionResult> DeleteAddressAsync(Guid id)
    {
        var address = await _addressService.GetAddressAsync(id);
        CheckIfEntityExists(address, "Given address does not exist");
        await _addressService.DeleteAddressAsync(address);
        return Ok();
    }
}