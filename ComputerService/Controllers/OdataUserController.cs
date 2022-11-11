using AutoMapper;
using ComputerService.Entities;
using ComputerService.Interfaces;
using ComputerService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace ComputerService.Controllers;
[Route("api/v1/odataUsers")]
[ApiVersion("1.0")]
[Authorize]
[ApiController]
public class OdataUserController : BaseController<User>
{
    private readonly IOdataUserService _userService;
    public OdataUserController(IOdataUserService odataUserService, IPaginationService paginationService, IMapper mapper, ILogger<BaseController<User>> logger) : base(paginationService, mapper, logger)
    {
        _userService = odataUserService;
    }

    [HttpGet]
    [EnableQuery(PageSize = 2)]
    public async Task<ActionResult<IEnumerable<UserViewModel>>> GetAllUsersAsync()
    {
        var users = await _userService.GetAllUsersAsync();
        var mappedUsers = Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(users);
        return Ok(mappedUsers);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Response<UserViewModel>>> GetUserAsync(Guid id)
    {
        var user = await _userService.GetUserAsync(id);
        CheckIfEntityExists(user, "Given user does not exist");
        return Ok(new Response<UserViewModel>(Mapper.Map<UserViewModel>(user)));
    }

    [HttpPost]
    public async Task<IActionResult> AddUserAsync([FromBody] CreateUserModel createUserModel)
    {
        var user = Mapper.Map<User>(createUserModel);
        await _userService.AddUserAsync(user);
        return Ok();
    }

    [HttpPatch("{id:guid}")]
    public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UpdateUserModel updateUserModel)
    {
        var user = await _userService.GetUserAsync(id);
        CheckIfEntityExists(user, "Given user does not exist");
        var updatedUser = Mapper.Map(updateUserModel, user);
        await _userService.UpdateUserAsync(updatedUser);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUserAsync(Guid id)
    {
        var user = await _userService.GetUserAsync(id);
        CheckIfEntityExists(user, "Given user does not exist");
        await _userService.DeleteUserAsync(user);
        return Ok();
    }
}