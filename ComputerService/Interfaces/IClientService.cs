using ComputerService.Entities;
using ComputerService.Models;

namespace ComputerService.Interfaces;
public interface IClientService
{
    Task<PagedList<Client>> GetAllClientsAsync(ParametersModel parameters);
    Task<Client> GetClientAsync(Guid id);
    Task AddClientAsync(Client client);
    Task UpdateClientAsync(Client client);
    Task DeleteClientAsync(Client client);
}

