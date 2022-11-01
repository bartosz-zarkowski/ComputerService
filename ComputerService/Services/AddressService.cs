using AutoMapper;
using ComputerService.Data;
using ComputerService.Entities;
using ComputerService.Interfaces;
using ComputerService.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ComputerService.Services;
public class ClientService : BaseEntityService<Client>, IClientService
{
    public ClientService(ComputerServiceContext context, IValidator<Client> validator, IMapper mapper) : base(context, validator, mapper) { }

    public async Task<PagedList<Client>> GetAllClientsAsync(ParametersModel parametersModel)
    {
        return await PagedList<Client>.ToPagedListAsync(FindAll(), parametersModel.PageNumber, parametersModel.PageSize);
    }

    public async Task<Client> GetClientAsync(Guid id)
    {
        return await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddClientAsync(Client client)
    {
        await ValidateEntityAsync(client);
        await CreateAsync(client);
    }

    public async Task UpdateClientAsync(Client client)
    {
        await ValidateEntityAsync(client);
        await UpdateAsync(client);
    }

    public async Task DeleteClientAsync(Client client)
    {
        await DeleteAsync(client);
    }
}
