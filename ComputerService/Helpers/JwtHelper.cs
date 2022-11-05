using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ComputerService.Helpers;

public class JwtHelper
{
    public static JwtSecurityToken GetJwtToken
    (
        string signingKey,
        string issuer,
        string audience,
        TimeSpan expiration,
        Claim[] claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken
        (
            issuer: issuer,
            audience: audience,
            expires: DateTime.UtcNow.Add(expiration),
            claims: claims,
            signingCredentials: credentials
        );
        return token;
    }
}