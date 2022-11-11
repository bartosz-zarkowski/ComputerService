using AutoMapper;
using ComputerService.Data;
using ComputerService.Entities;
using ComputerService.Exceptions;
using ComputerService.Helpers;
using ComputerService.Interfaces;
using ComputerService.Models;
using ComputerService.Repository;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ComputerService.Services;

public class AuthenticationService : BaseRepository<User>, IAuthenticationService
{
    protected IMapper Mapper;
    private readonly IConfiguration _configuration;

    public AuthenticationService(ComputerServiceContext context, IMapper mapper, IConfiguration configuration) : base(context)
    {
        Mapper = mapper;
        _configuration = configuration;
    }

    public async Task<JwtUserModel> GetUserAsync(AuthenticateRequestModel loginUser)
    {
        User user = await FindByCondition(x => x.Email == loginUser.Email).FirstOrDefaultAsync();
        CheckIfUserExists(user);
        IsAuthenticated(loginUser.Password, user.Password);
        IsUserActive(user.IsActive);
        return Mapper.Map<User, JwtUserModel>(user);
    }

    public JwtSecurityToken GetToken(JwtUserModel user)
    {
        var signingKey = _configuration["Jwt:Key"];
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var expiration = TimeSpan.FromMinutes(Convert.ToDouble(_configuration["Jwt:TokenTimeoutMinutes"]));
        var claims = GetClaims(user);

        return JwtHelper.GetJwtToken(signingKey, issuer, audience, expiration, claims.ToArray());
    }

    public void IsAuthenticated(string loginUserPassword, string userPassword)
    {
        if (loginUserPassword != userPassword)
        {
            throw new AuthenticationException("Incorrect password");
        }
    }

    public void IsUserActive(bool isUserActive)
    {
        if (!isUserActive)
        {
            throw new AuthenticationException("User is disabled");
        }
    }

    public void CheckIfUserExists(User user)
    {
        if (user == null)
        {
            throw new NotFoundException("User does not exist");
        }
    }

    public List<Claim> GetClaims(JwtUserModel user)
    {
        return new List<Claim>
        {
            new Claim("id", user.Id.ToString()),
            new Claim("firstName", user.FirstName),
            new Claim("lastName", user.LastName),
            new Claim("email", user.Email),
            new Claim("role", user.Role.ToString()),
        };
    }
}