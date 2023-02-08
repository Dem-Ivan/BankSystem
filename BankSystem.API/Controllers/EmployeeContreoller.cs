using BankSystem.App.Cases;
using BankSystem.App.DTO;
using BankSystem.Domain.Models.Templates;
using Microsoft.AspNetCore.Mvc;
using System;


namespace BankSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        RegisterEmployeeCase _employeeCase;
        ContractTemplate _template = ContractTemplate.GetInstance();
        ContractCase _contractCase;

        public EmployeeController(RegisterEmployeeCase employeeCase, ContractCase contractCase)
        {
            _employeeCase = employeeCase;
            _contractCase = contractCase;
        }

        [HttpGet]
        public ActionResult<EmployeeResponse> GetEmployee([FromQuery] Guid employeeId)
        {            
            return _employeeCase.Get(employeeId);
        }

        [HttpPost("AddEmployee")]
        public ActionResult<Guid> AddEmployee([FromBody] EmployeeRequest employeeReq)
        {
            return _employeeCase.AddEmployee(employeeReq);
        }

        [HttpPost("createNewContractWith")]       
        public ActionResult<ContractResponse> CreateNewContractWith([FromQuery] Guid clientId, Guid authorId)
        {//при наличии аутентификации - authorId берем из контекста запроса

            var contractId = _contractCase.CreateNewcontract(_template, authorId, clientId);
            return _contractCase.CompleteContract(clientId, contractId);
        }

        [HttpPut("SignContract")]
        public IActionResult SignContract([FromQuery] Guid singerId, Guid contractId)
        {//при наличии аутентификации - подписанта (singerId) берем из контекста запроса

            _contractCase.SignContract(singerId, contractId);

            return Ok();
        }
    }
}
