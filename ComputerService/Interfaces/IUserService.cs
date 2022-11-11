using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Models;

namespace ComputerService.Interfaces;

public interface IUserService
{
    IQueryable<User> GetAllUsersAsync(ParametersModel parameters, UserSortEnum? sortOrder);
    Task<PagedList<User>> GetPagedUsersAsync(ParametersModel parameters, UserSortEnum? sortOrder);
    Task<User> GetUserAsync(Guid id);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
}