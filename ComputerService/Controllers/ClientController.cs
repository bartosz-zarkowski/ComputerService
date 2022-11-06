using AutoMapper;
using ComputerService.Entities;
using ComputerService.Interfaces;
using ComputerService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerService.Controllers;

[Route("api/v1/clients")]
[ApiVersion("1.0")]
[Authorize(Roles = "Administrator")]
[ApiController]
public class ClientController : BaseController<Client>
{
    private readonly IClientService _clientService;
    public ClientController(IClientService clientService, IPaginationService paginationService, IMapper mapper, ILogger<BaseController<Client>> logger) : base(paginationService, mapper, logger)
    {
        _clientService = clientService;
    }

    [Authorize(Roles = "Receiver")]
    [HttpGet]
    public async Task<ActionResult<PagedListViewModel<PagedResponse<ClientViewModel>>>> GetAllClientsAsync([FromQuery] ParametersModel parameters)
    {
        var clients = await _clientService.GetAllClientsAsync(parameters);
        Logger.LogInformation("Returned {Count} clients from database. ", clients.Count());

        var mappedClients = PaginationService.ToPagedListViewModelAsync<Client, ClientViewModel>(clients);
        var pagedResponse = PaginationService.CreatePagedResponse(mappedClients);

        return Ok(pagedResponse);
    }

    [Authorize(Roles = "Receiver, Technician")]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Response<ClientViewModel>>> GetClientAsync(Guid id)
    {
        var client = await _clientService.GetClientAsync(id);
        CheckIfEntityExists(client, "Given client does not exist");
        return Ok(new Response<ClientViewModel>(Mapper.Map<ClientViewModel>(client)));
    }

    [Authorize(Roles = "Receiver")]
    [HttpPost]
    public async Task<IActionResult> AddClientAsync([FromBody] CreateClientModel createClientModel)
    {
        var client = Mapper.Map<Client>(createClientModel);
        await _clientService.AddClientAsync(client);
        return Ok();
    }

    [Authorize(Roles = "Receiver, Technician")]
    [HttpPatch("{id:guid}")]
    public async Task<ActionResult> UpdateClient(Guid id, [FromBody] UpdateClientModel updateClientModel)
    {
        var client = await _clientService.GetClientAsync(id);
        CheckIfEntityExists(client, "Given client does not exist");
        var updatedClient = Mapper.Map(updateClientModel, client);
        await _clientService.UpdateClientAsync(updatedClient);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteClientAsync(Guid id)
    {
        var client = await _clientService.GetClientAsync(id);
        CheckIfEntityExists(client, "Given client does not exist");
        await _clientService.DeleteClientAsync(client);
        return Ok();
    }
}