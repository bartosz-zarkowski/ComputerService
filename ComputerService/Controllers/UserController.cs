using AutoMapper;
using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Interfaces;
using ComputerService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerService.Controllers;
[Route("api/v1/users")]
[ApiVersion("1.0")]
[Authorize]
[ApiController]
public class UserController : BaseController<User>
{
    private readonly IUserService _userService;
    public UserController(IUserService userService, IPaginationService paginationService, IMapper mapper, ILogger<BaseController<User>> logger) : base(paginationService, mapper, logger)
    {
        _userService = userService;
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
        var salt = "password salt";
        user.Salt = salt;
        await _userService.AddUserAsync(user);
        return Ok();
    }

    [HttpPatch("{id:guid}")]
    [Authorize(Roles = "Administrator, Receiver, Technician")]
    public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UpdateUserModel updateUserModel)
    {
        var user = await _userService.GetUserAsync(id);
        CheckIfEntityExists(user, "Given user does not exist");
        var updatedUser = Mapper.Map(updateUserModel, user);
        await _userService.UpdateUserAsync(updatedUser);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteUserAsync(Guid id)
    {
        var user = await _userService.GetUserAsync(id);
        CheckIfEntityExists(user, "Given user does not exist");
        await _userService.DeleteUserAsync(user);
        return Ok();
    }
}