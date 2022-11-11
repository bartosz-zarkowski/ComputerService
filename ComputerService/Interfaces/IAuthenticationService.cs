using ComputerService.Entities;
using ComputerService.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ComputerService.Interfaces;

public interface IAuthenticationService
{
    Task<JwtUserModel> GetUserAsync(AuthenticateRequestModel loginUser);

    void IsAuthenticated(string loginUserPassword, string userPassword);

    void IsUserActive(bool isUserActive);

    void CheckIfUserExists(User user);

    List<Claim> GetClaims(JwtUserModel user);

    JwtSecurityToken GetToken(JwtUserModel user);
}