namespace ComputerService.Security;
public interface IPasswordHashingService
{
    public byte[] GetSalt();

    public string GetSaltAsString();

    public string HashPassword(string password, string salt);

    public void ValidatePassword(string password, string savedPasswordHash, string savedSalt);
}
