using Kolokwium.DTOs;
using Kolokwium.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly ClientService _clientService;

    public ClientsController(ClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet("{id}")]
    public ActionResult<ClientDto> GetClientWithSubscriptions(int id)
    {
        var client = _clientService.GetClientWithSubscriptions(id);
        if (client == null)
        {
            return NotFound();
        }
        return Ok(client);
    }
}
