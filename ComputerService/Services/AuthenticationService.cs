using AutoMapper;
using ComputerService.Data;
using ComputerService.Entities;
using ComputerService.Exceptions;
using ComputerService.Helpers;
using ComputerService.Interfaces;
using ComputerService.Models;
using ComputerService.Repository;
using ComputerService.Security;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ComputerService.Services;

public class AuthenticationService : BaseRepository<User>, IAuthenticationService
{
    protected IMapper Mapper;
    private readonly IConfiguration _configuration;
    private readonly IPasswordHashingService _passwordHashingService;

    public AuthenticationService(ComputerServiceContext context, IMapper mapper, IConfiguration configuration, IPasswordHashingService passwordHashingService) : base(context)
    {
        Mapper = mapper;
        _configuration = configuration;
        _passwordHashingService = passwordHashingService;
    }

    public async Task<JwtUserModel> GetUserAsync(AuthenticateRequestModel loginUser)
    {
        User user = await FindByCondition(x => x.Email == loginUser.Email).FirstOrDefaultAsync();
        CheckIfUserExists(user);
        IsAuthenticated(loginUser.Password, user.Password, user.Salt);
        IsUserActive(user.IsActive);
        return Mapper.Map<User, JwtUserModel>(user);
    }

    public void IsAuthenticated(string password, string savedPasswordHash, string savedSalt)
    {
        _passwordHashingService.ValidatePassword(password, savedPasswordHash, savedSalt);
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

    public JwtSecurityToken GetToken(JwtUserModel user)
    {
        var signingKey = _configuration["Jwt:Key"];
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var expiration = TimeSpan.FromMinutes(Convert.ToDouble(_configuration["Jwt:TokenTimeoutMinutes"]));
        var claims = GetClaims(user);

        return JwtHelper.GetJwtToken(signingKey, issuer, audience, expiration, claims.ToArray());
    }
}