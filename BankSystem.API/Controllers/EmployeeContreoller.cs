using BankSystem.App.Cases;
using BankSystem.App.DTO;
using BankSystem.Domain.Models.Templates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

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

        [HttpPost]
        public ActionResult<Guid> AddEmployee([FromBody] EmployeeRequest employeeReq)
        {
            return _employeeCase.AddEmployee(employeeReq);
        }

        [HttpPost("createNewContractWith")]       
        public ActionResult<ContractResponse> CreateNewContractWith([FromBody] Guid clientId, Guid authorId)
        {//при наличии аутентификации - authorId берем из контекста запроса

            var contractId = _contractCase.CreateNewcontract(_template, authorId);
            return _contractCase.CompleteContract(clientId, contractId);
        }

        [HttpPut]
        public IActionResult SignContract([FromBody] Guid singerId, Guid contractId)
        {//при наличии аутентификации - подписанта (singerId) берем из контекста запроса

            _contractCase.SignContract(singerId, contractId);

            return Ok();
        }
    }
}
