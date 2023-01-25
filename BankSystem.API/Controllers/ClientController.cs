using BankSystem.App.Cases;
using BankSystem.App.DTO;
using BankSystem.Domain.Models.Templates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace BankSystem.API.Controllers
{
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
        public ActionResult<EmployeeResponse> GetClient([FromQuery] Guid employeeId)
        {
            return null;
        }

        [HttpPost]
        public ActionResult<Guid> AddClient([FromBody] EmployeeRequest employeeReq)
        {
            return null;
        }

        [HttpPut]
        public ActionResult<Guid> СonfirmContract([FromBody] Guid clientId, Guid contractId)
        {//при наличии аутентификации - контерагента (clientId) берем из контекста запроса

            return _contractCase.СonfirmAcquaintance(clientId, contractId);
        }
    }
}
