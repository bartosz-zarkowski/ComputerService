using ComputerService.Models;
using ComputerService.Security;
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
    private readonly ITokenManager _tokenManager;

    public AuthenticationController(IAuthenticationService authenticationService, ITokenManager tokenManager)
    {
        _authenticationService = authenticationService;
        _tokenManager = tokenManager;
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

    [HttpPost("authentication/cancel")]
    public async Task<IActionResult> CancelAccessToken()
    {
        await _tokenManager.DeactivateCurrentAsync();

        return NoContent();
    }
}