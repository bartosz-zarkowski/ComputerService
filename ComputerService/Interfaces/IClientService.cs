using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace ComputerService.Interfaces;

public interface IClientService
{
    IQueryable<Client> GetAllClients(ParametersModel parameters, ClientSortEnum? sortOrder);
    Task<PagedList<Client>> GetPagedClientsAsync(ParametersModel parameters, ClientSortEnum? sortOrder);
    Task<Client> GetClientAsync(Guid id);
    Task AddClientAsync(Client client);
    Task UpdateClientAsync(Client client, JsonPatchDocument<UpdateClientModel> updateClientModelJpd);
    Task DeleteClientAsync(Client client);
}