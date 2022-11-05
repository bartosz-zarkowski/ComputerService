using ComputerService.Interfaces;
using ComputerService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace ComputerService.Controllers;

[Route("api/v1/")]
[ApiVersion("1.0")]
[Authorize]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("authentication")]
    public async Task<object> Authenticate([FromQuery] AuthenticateRequestModel loginUser)
    {
        var user = await _authenticationService.GetUserAsync(loginUser);
        var token = _authenticationService.GetToken(user);

        return new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expires = token.ValidTo
        };
    }
}