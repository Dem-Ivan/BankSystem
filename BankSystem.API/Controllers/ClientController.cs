using BankSystem.App.Cases;
using BankSystem.App.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    RegisterClientCase _clientCase;
    ContractCase _contractCase;

    public ClientController(RegisterClientCase clientCase, ContractCase contractCase)
    {
        _clientCase = clientCase;
        _contractCase = contractCase;
    }

    [HttpGet]
    public async Task<ActionResult<ClientResponse>> GetClient([FromQuery] Guid clientId)
    {
        return await _clientCase.Get(clientId);
    }

    [HttpPost("AddClient")]
    public async Task<ActionResult<Guid>> AddClient([FromBody] ClientRequest clientReq)
    {
        return await _clientCase.AddClient(clientReq);
    }

    [HttpPut("СonfirmContract")]
    public async Task<ActionResult<Guid>> СonfirmContract([FromQuery] Guid clientId, Guid contractId)
    {//при наличии аутентификации - контерагента (clientId) берем из контекста запроса

        return await _contractCase.СonfirmAcquaintance(clientId, contractId);
    }
}