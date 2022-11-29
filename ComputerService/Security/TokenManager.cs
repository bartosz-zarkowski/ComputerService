using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;

namespace ComputerService.Security;

public class TokenManager : ITokenManager
{
    private readonly IDistributedCache _cache;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public TokenManager(IDistributedCache cache, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _cache = cache;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

    public async Task<bool> IsCurrentActiveToken()
        => await IsActiveAsync(GetCurrentAsync());

    public async Task DeactivateCurrentAsync()
        => await DeactivateAsync(GetCurrentAsync());

    public async Task<bool> IsActiveAsync(string token)
        => await _cache.GetStringAsync(GetKey(token)) == null;

    public async Task DeactivateAsync(string token)
        => await _cache.SetStringAsync(GetKey(token), " ", new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(Convert.ToDouble(_configuration["Jwt:TokenTimeoutMinutes"]))
        });

    private string GetCurrentAsync()
    {
        var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["authorization"];

        return authorizationHeader == StringValues.Empty
            ? String.Empty
            : authorizationHeader.Single().Split(" ").Last();
    }

    public Guid GetCurrentUserId() => new(new JwtSecurityTokenHandler().ReadJwtToken(GetCurrentAsync()).Claims
        .First(c => c.Type == "id").Value);

    private static string GetKey(string token) => $"tokens:{token}: deactivated";
}