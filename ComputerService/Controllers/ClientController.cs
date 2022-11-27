using AutoMapper;
using ComputerService.Entities;
using ComputerService.Enums;
using ComputerService.Interfaces;
using ComputerService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ComputerService.Controllers;

[Route("api/v1/clients")]
[ApiVersion("1.0")]
[Authorize]
[ApiController]
public class ClientController : BaseController<Client>
{
    private readonly IClientService _clientService;
    public ClientController(IClientService clientService, IPaginationService paginationService, IMapper mapper, ILogger<BaseController<Client>> logger) : base(paginationService, mapper, logger)
    {
        _clientService = clientService;
    }

    [Authorize(Roles = "Administrator, Receiver")]
    [HttpGet]
    public async Task<ActionResult<PagedListViewModel<PagedResponse<ClientViewModel>>>> GetAllClientsAsync([FromQuery] ParametersModel parameters, [FromQuery] ClientSortEnum? sortOrder)
    {
        var clients = await _clientService.GetPagedClientsAsync(parameters, sortOrder);
        Logger.LogInformation("Returned {Count} clients from database. ", clients.Count());

        var mappedClients = PaginationService.ToPagedListViewModelAsync<Client, ClientViewModel>(clients);
        var pagedResponse = PaginationService.CreatePagedResponse(mappedClients, parameters, sortOrder);

        return Ok(pagedResponse);
    }

    [Authorize(Roles = "Administrator, Receiver, Technician")]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Response<ClientViewModel>>> GetClientAsync(Guid id)
    {
        var client = await _clientService.GetClientAsync(id);
        CheckIfEntityExists(client, "Given client does not exist");
        return Ok(new Response<ClientViewModel>(Mapper.Map<ClientViewModel>(client)));
    }

    [Authorize(Roles = "Administrator, Receiver")]
    [HttpPost]
    public async Task<IActionResult> AddClientAsync([FromBody] CreateClientModel createClientModel)
    {
        var client = Mapper.Map<Client>(createClientModel);
        await _clientService.AddClientAsync(client);
        return Ok();
    }

    [Authorize(Roles = "Administrator, Receiver, Technician")]
    [HttpPatch("{id:guid}")]
    public async Task<ActionResult> UpdateClient(Guid id, [FromBody] JsonPatchDocument<UpdateClientModel> updateClientModelJpd)
    {
        var client = await _clientService.GetClientAsync(id);
        CheckIfEntityExists(client, "Given client does not exist");
        await _clientService.UpdateClientAsync(client, updateClientModelJpd);
        return Ok();
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteClientAsync(Guid id)
    {
        var client = await _clientService.GetClientAsync(id);
        CheckIfEntityExists(client, "Given client does not exist");
        await _clientService.DeleteClientAsync(client);
        return Ok();
    }
}