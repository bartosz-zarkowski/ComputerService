using AutoMapper;
using ComputerService.Data;
using ComputerService.Entities;
using ComputerService.Interfaces;
using ComputerService.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Services;
public class UserService : BaseEntityService<User>, IUserService
{
    public UserService(ComputerServiceContext context, IValidator<User> validator, IMapper mapper) : base(context, validator, mapper) { }

    public async Task<PagedList<User>> GetAllUsersAsync(ParametersModel parametersModel)
    {
        return await PagedList<User>.ToPagedListAsync(FindAll(), parametersModel.PageNumber, parametersModel.PageSize);
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
