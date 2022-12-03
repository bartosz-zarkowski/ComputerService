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

[Route("api/v1/addresses")]
[ApiVersion("1.0")]
[Authorize]
[ApiController]
public class AddressController : BaseController<Address>
{
    private readonly IAddressService _addressService;
    public AddressController(IAddressService addressService, IPaginationService paginationService, IMapper mapper, ILogger<BaseController<Address>> logger, IUserTrackingService userTrackingService) : base(paginationService, mapper, logger, userTrackingService)
    {
        _addressService = addressService;
    }

    [HttpGet]
    [Authorize(Roles = "Administrator, Receiver")]
    public async Task<ActionResult<PagedListViewModel<PagedResponse<AddressViewModel>>>> GetAllAddressesAsync([FromQuery] ParametersModel parameters, [FromQuery] AddressSortEnum? sortOrder)
    {
        var addresses = await _addressService.GetPagedAddressesAsync(parameters, sortOrder);
        Logger.LogInformation("Returned {Count} addresses from database. ", addresses.Count());

        var mappedAddresses = PaginationService.ToPagedListViewModelAsync<Address, AddressViewModel>(addresses);
        var pagedResponse = PaginationService.CreatePagedResponse(mappedAddresses, parameters, sortOrder);

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
        await UserTrackingService?.AddUserTrackingAsync(TrackingActionTypeEnum.CreateAddress, address.Id.ToString(), $"Created address to user with id '{address.Id}'")!;
        return Ok(new { addressId = address.Id });
    }

    [HttpPatch("{id:guid}")]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<ActionResult> UpdateAddress(Guid id, [FromBody] JsonPatchDocument<UpdateAddressModel> updateAddressModelJpd)
    {
        var address = await _addressService.GetAddressAsync(id);
        CheckIfEntityExists(address, "Given address does not exist");
        await _addressService.UpdateAddressAsync(address, updateAddressModelJpd);
        await UserTrackingService?.AddUserTrackingAsync(TrackingActionTypeEnum.UpdateAddress, address.Id.ToString(), $"Updated address of user with id '{address.Id}'")!;
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator, Receiver")]
    public async Task<IActionResult> DeleteAddressAsync(Guid id)
    {
        var address = await _addressService.GetAddressAsync(id);
        CheckIfEntityExists(address, "Given address does not exist");
        await _addressService.DeleteAddressAsync(address);
        await UserTrackingService?.AddUserTrackingAsync(TrackingActionTypeEnum.DeleteAddress, address.Id.ToString(), $"Deleted address from user with id '{address.Id}'")!;
        return Ok();
    }
}