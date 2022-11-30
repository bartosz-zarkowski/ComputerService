using AutoMapper;
using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Interfaces;
using ComputerService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerService.Controllers;

[Route("api/v1/userTrackings")]
[ApiVersion("1.0")]
[Authorize(Roles = "Administrator")]
[ApiController]
public class UserTrackingController : BaseController<UserTracking>
{
    private readonly IUserTrackingService _userTrackingService;
    public UserTrackingController(IUserTrackingService userTrackingService, IPaginationService paginationService, IMapper mapper, ILogger<BaseController<UserTracking>> logger) : base(paginationService, mapper, logger)
    {
        _userTrackingService = userTrackingService;
    }

    [HttpGet]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<ActionResult<PagedListViewModel<PagedResponse<AccessoryViewModel>>>> GetAllAccessoriesAsync([FromQuery] ParametersModel parameters, [FromQuery] UserTrackingSortEnum? sortOrder)
    {
        var userTrackings = await _userTrackingService.GetPagedUserTrackingsAsync(parameters, sortOrder);
        Logger.LogInformation("Returned {Count} user trackings from database. ", userTrackings.Count());

        var mappedAccessories = PaginationService.ToPagedListViewModelAsync<UserTracking, UserTrackingViewModel>(userTrackings);
        var pagedResponse = PaginationService.CreatePagedResponse(mappedAccessories, parameters, sortOrder);

        return Ok(pagedResponse);
    }
}