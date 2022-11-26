using ComputerService.Entities;
using ComputerService.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ComputerService.Security;

public interface IAuthenticationService
{
    Task<JwtUserModel> GetUserAsync(AuthenticateRequestModel loginUser);

    void IsAuthenticated(string password, string savedPasswordHash, string savedSalt);

    void IsUserActive(bool isUserActive);

    void CheckIfUserExists(User user);

    List<Claim> GetClaims(JwtUserModel user);

    JwtSecurityToken GetToken(JwtUserModel user);
}