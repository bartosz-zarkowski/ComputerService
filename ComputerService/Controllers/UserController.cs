using AutoMapper;
using ComputerService.Entities;
using ComputerService.Entities.Enums;
using ComputerService.Enums;
using ComputerService.Interfaces;
using ComputerService.Models;
using ComputerService.Security;
using ComputerService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ComputerService.Controllers;
[Route("api/v1/users")]
[ApiVersion("1.0")]
[Authorize]
[ApiController]
public class UserController : BaseController<User>
{
    private readonly IUserService _userService;
    private readonly IPasswordHashingService _passwordHashingService;
    public UserController(IUserService userService, IPaginationService paginationService, IMapper mapper, ILogger<BaseController<User>> logger, IPasswordHashingService passwordHashingService, IUserTrackingService userTrackingService) : base(paginationService, mapper, logger, userTrackingService)
    {
        _userService = userService;
        _passwordHashingService = passwordHashingService;
    }

    [HttpGet]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<ActionResult<PagedListViewModel<PagedResponse<UserViewModel>>>> GetAllUsersAsync([FromQuery] ParametersModel parameters, [FromQuery] UserSortEnum? sortOrder)
    {
        var users = await _userService.GetPagedUsersAsync(parameters, sortOrder);
        Logger.LogInformation("Returned {Count} users from database. ", users.Count());

        var mappedUsers = PaginationService.ToPagedListViewModelAsync<User, UserViewModel>(users);
        var pagedResponse = PaginationService.CreatePagedResponse(mappedUsers, parameters, sortOrder);

        return Ok(pagedResponse);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<ActionResult<Response<UserViewModel>>> GetUserAsync(Guid id)
    {
        var user = await _userService.GetUserAsync(id);
        CheckIfEntityExists(user, "Given user does not exist");
        return Ok(new Response<UserViewModel>(Mapper.Map<UserViewModel>(user)));
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> AddUserAsync([FromBody] CreateUserModel createUserModel)
    {
        var user = Mapper.Map<User>(createUserModel);
        user.Salt = _passwordHashingService.GetSaltAsString();
        user.Password = _passwordHashingService.HashPassword(user.Password, user.Salt);
        await _userService.AddUserAsync(user);
        await UserTrackingService?.AddUserTrackingAsync(TrackingActionTypeEnum.CreateUser, user.Id.ToString()
            , $"Created user: {user.FirstName} {user.LastName}")!;
        return Ok();
    }

    [HttpPatch("{id:guid}")]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<ActionResult> UpdateUser(Guid id, [FromBody] JsonPatchDocument<UpdateUserModel> updateUserModelJpd)
    {
        var user = await _userService.GetUserAsync(id);
        CheckIfEntityExists(user, "Given user does not exist");
        await _userService.UpdateUserAsync(user, updateUserModelJpd);
        await UserTrackingService?.AddUserTrackingAsync(TrackingActionTypeEnum.UpdateUser, user.Id.ToString()
            , $"Updated user: {user.FirstName} {user.LastName}")!;
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteUserAsync(Guid id)
    {
        var user = await _userService.GetUserAsync(id);
        CheckIfEntityExists(user, "Given user does not exist");
        await _userService.DeleteUserAsync(user);
        await UserTrackingService?.AddUserTrackingAsync(TrackingActionTypeEnum.DeleteUser, user.Id.ToString()
            , $"Deleted user: {user.FirstName} {user.LastName}")!;
        return Ok();
    }
}