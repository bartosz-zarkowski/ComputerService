using ComputerService.Entities;

namespace ComputerService.Interfaces;
public interface IOdataUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserAsync(Guid id);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
}

