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
    public ActionResult<ClientResponse> GetClient([FromQuery] Guid clientId)
    {
        return _clientCase.Get(clientId);
    }

    [HttpPost("AddClient")]
    public ActionResult<Guid> AddClient([FromBody] ClientRequest clientReq)
    {
        return _clientCase.AddClient(clientReq);
    }

    [HttpPut("СonfirmContract")]
    public ActionResult<Guid> СonfirmContract([FromQuery] Guid clientId, Guid contractId)
    {//при наличии аутентификации - контерагента (clientId) берем из контекста запроса

        return _contractCase.СonfirmAcquaintance(clientId, contractId);
    }
}