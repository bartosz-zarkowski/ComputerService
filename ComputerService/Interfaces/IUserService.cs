using ComputerService.Entities;
using ComputerService.Models;

namespace ComputerService.Interfaces;
public interface IUserService
{
    Task<PagedList<User>> GetAllUsersAsync(ParametersModel parameters);
    Task<User> GetUserAsync(Guid id);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
}

