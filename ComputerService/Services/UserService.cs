using AutoMapper;
using Castle.Core.Internal;
using ComputerService.Data;
using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Interfaces;
using ComputerService.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Services;
public class UserService : BaseEntityService<User>, IUserService
{
    public UserService(ComputerServiceContext context, IValidator<User> validator, IMapper mapper) : base(context, validator, mapper) { }

    public IQueryable<User> GetAllUsersAsync(ParametersModel parameters, UserSortEnum? sortOrder)
    {
        var users = FindAll();
        if (sortOrder != null)
        {
            bool asc = (bool)parameters.asc;
            users = Enum.IsDefined(typeof(UserSortEnum), sortOrder)
                ? sortOrder switch
                {
                    UserSortEnum.CreatedAt => asc
                        ? users.OrderBy(user => user.CreatedAt)
                        : users.OrderByDescending(user => user.CreatedAt),
                    UserSortEnum.UpdatedAt => asc
                        ? users.OrderBy(user => user.UpdatedAt)
                        : users.OrderByDescending(user => user.UpdatedAt),
                    UserSortEnum.FirstName => asc
                        ? users.OrderBy(user => user.FirstName)
                        : users.OrderByDescending(user => user.FirstName),
                    UserSortEnum.LastName => asc
                        ? users.OrderBy(user => user.LastName)
                        : users.OrderByDescending(user => user.LastName),
                    UserSortEnum.Email => asc
                        ? users.OrderBy(user => user.Email)
                        : users.OrderByDescending(user => user.Email),
                    UserSortEnum.PhoneNumber => asc
                        ? users.OrderBy(user => user.PhoneNumber)
                        : users.OrderByDescending(user => user.PhoneNumber),
                    UserSortEnum.IsActive => asc
                        ? users.OrderBy(user => user.IsActive)
                        : users.OrderByDescending(user => user.IsActive),
                    UserSortEnum.Role => asc
                        ? users.OrderBy(user => user.Role)
                        : users.OrderByDescending(user => user.Role),
                }
                : throw new ArgumentException();
        }
        if (!parameters.searchString.IsNullOrEmpty())
        {
            users = users.Where(user => user.FirstName.Contains(parameters.searchString) ||
                                        user.LastName.Contains(parameters.searchString) ||
                                        user.PhoneNumber.Contains(parameters.searchString) ||
                                        user.Email.Contains(parameters.searchString));
        }
        return users;
    }

    public async Task<PagedList<User>> GetPagedUsersAsync(ParametersModel parameters, UserSortEnum? sortOrder)
    {
        return await PagedList<User>.ToPagedListAsync(GetAllUsersAsync(parameters, sortOrder), parameters);
    }

    public async Task<User> GetUserAsync(Guid id)
    {
        return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddUserAsync(User user)
    {
        await ValidateEntityAsync(user);
        await CreateAsync(user);
    }

    public async Task UpdateUserAsync(User user)
    {
        await ValidateEntityAsync(user);
        await UpdateAsync(user);
    }

    public async Task DeleteUserAsync(User user)
    {
        await DeleteAsync(user);
    }
}