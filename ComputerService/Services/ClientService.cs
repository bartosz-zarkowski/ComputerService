using AutoMapper;
using ComputerService.Data;
using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Interfaces;
using ComputerService.Models;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using static System.String;

namespace ComputerService.Services;

public class ClientService : BaseEntityService<Client>, IClientService
{
    public ClientService(ComputerServiceContext context, IValidator<Client> validator, IMapper mapper) : base(context, validator, mapper) { }

    public IQueryable<Client> GetAllClients(ParametersModel parameters, ClientSortEnum? sortOrder)
    {
        var clients = FindAll();
        if (sortOrder != null)
        {
            var asc = parameters.asc ?? true;
            clients = sortOrder switch
            {
                ClientSortEnum.CreatedAt => asc
                    ? clients.OrderBy(client => client.CreatedAt)
                    : clients.OrderByDescending(client => client.CreatedAt),
                ClientSortEnum.UpdatedAt => asc
                    ? clients.OrderBy(client => client.UpdatedAt)
                    : clients.OrderByDescending(client => client.UpdatedAt),
                ClientSortEnum.FirstName => asc
                    ? clients.OrderBy(client => client.FirstName)
                    : clients.OrderByDescending(client => client.FirstName),
                ClientSortEnum.LastName => asc
                    ? clients.OrderBy(client => client.LastName)
                    : clients.OrderByDescending(client => client.LastName),
                ClientSortEnum.Email => asc
                    ? clients.OrderBy(client => client.Email)
                    : clients.OrderByDescending(client => client.Email),
                ClientSortEnum.PhoneNumber => asc
                    ? clients.OrderBy(client => client.PhoneNumber)
                    : clients.OrderByDescending(client => client.PhoneNumber),
                _ => throw new ArgumentException()
            };
        }
        if (!IsNullOrEmpty(parameters.searchString))
        {
            clients = clients.Where(client => client.FirstName.Contains(parameters.searchString) ||
                                              client.LastName.Contains(parameters.searchString) ||
                                              client.Email.Contains(parameters.searchString) ||
                                              client.PhoneNumber.Contains(parameters.searchString));
        }
        return clients;
    }

    public async Task<PagedList<Client>> GetPagedClientsAsync(ParametersModel parameters, ClientSortEnum? sortOrder)
    {
        return await PagedList<Client>.ToPagedListAsync(GetAllClients(parameters, sortOrder), parameters);
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

    public async Task UpdateClientAsync(Client client, JsonPatchDocument<UpdateClientModel> updateClientModelJpd)
    {
        var mappedClient = Mapper.Map<UpdateClientModel>(client);
        updateClientModelJpd.ApplyTo(mappedClient);
        Mapper.Map(mappedClient, client);
        await ValidateEntityAsync(client);
        await UpdateAsync(client);
    }

    public async Task DeleteClientAsync(Client client)
    {
        await DeleteAsync(client);
    }
}