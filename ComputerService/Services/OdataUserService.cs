using AutoMapper;
using ComputerService.Data;
using ComputerService.Entities;
using ComputerService.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Services;
public class OdataUserService : BaseEntityService<User>, IOdataUserService
{
    public OdataUserService(ComputerServiceContext context, IValidator<User> validator, IMapper mapper) : base(context, validator, mapper) { }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await FindAll().ToListAsync();
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
