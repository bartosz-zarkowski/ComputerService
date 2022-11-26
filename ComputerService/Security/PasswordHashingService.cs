using ComputerService.Exceptions;
using System.Security.Cryptography;

namespace ComputerService.Security;
public class PasswordHashingService : IPasswordHashingService
{
    private readonly IConfiguration _configuration;
    public PasswordHashingService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public byte[] GetSalt()
    {
        var salt = new byte[16];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return salt;
    }

    public string GetSaltAsString()
    {
        var salt = GetSalt();
        return Convert.ToBase64String(salt);
    }

    public string HashPassword(string password, string saltString)
    {
        var salt = Convert.FromBase64String(saltString);
        var numberOfIterations = Convert.ToInt32(_configuration["Security:HashIterations"]);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, numberOfIterations, HashAlgorithmName.SHA256);

        byte[] hash = pbkdf2.GetBytes(20);

        byte[] hashBytes = new byte[36];

        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        string savedPasswordHash = Convert.ToBase64String(hashBytes);

        return savedPasswordHash;
    }

    public void ValidatePassword(string password, string savedPasswordHash, string savedSalt)
    {
        var passwordHash = HashPassword(password, savedSalt);

        if (passwordHash != savedPasswordHash)
        {
            throw new AuthenticationException("Invalid password!");
        }
    }
}