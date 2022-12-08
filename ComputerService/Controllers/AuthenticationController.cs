using AutoMapper;
using ComputerService.Models;
using ComputerService.Security;
using ComputerService.ViewModels;
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
    private readonly IMapper _mapper;
    public AuthenticationController(IAuthenticationService authenticationService, ITokenManager tokenManager, IMapper mapper)
    {
        _authenticationService = authenticationService;
        _tokenManager = tokenManager;
        _mapper = mapper;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("authentication")]
    public async Task<object> Authenticate([FromQuery] AuthenticateRequestModel loginUser)
    {
        var user = await _authenticationService.GetUserAsync(loginUser);
        var token = _authenticationService.GetToken(user);

        return new Response<object>(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(token),
            userData = _mapper.Map<UserViewModel>(user),
            expires = token.ValidTo
        });
    }

    [HttpPost("authentication/cancel")]
    public async Task<IActionResult> CancelAccessToken()
    {
        await _tokenManager.DeactivateCurrentAsync();

        return NoContent();
    }
}