using BankSystem.App.Cases;
using BankSystem.App.DTO;
using BankSystem.Domain.Models.Templates;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    ContractTemplate _template = ContractTemplate.GetInstance();

    [HttpGet]
    public async Task<ActionResult<EmployeeResponse>> GetEmployee([FromQuery] Guid employeeId, [FromServices] RegisterEmployeeCase employeeCase)
    {
        return await employeeCase.Get(employeeId);
    }

    [HttpPost("AddEmployee")]
    public async Task<ActionResult<Guid>> AddEmployee([FromBody] EmployeeRequest employeeReq, [FromServices] RegisterEmployeeCase employeeCase)
    {
        return await employeeCase.AddEmployee(employeeReq);
    }

    [HttpPost("createNewContractWith")]
    public async Task<ActionResult<ContractResponse>> CreateNewContractWith([FromQuery] Guid clientId, Guid authorId, [FromServices] ContractCase contractCase)
    {//при наличии аутентификации - authorId берем из контекста запроса

        var contractId = await contractCase.CreateNewcontract(_template, authorId, clientId);
        return await contractCase.CompleteContract(clientId, contractId);
    }

    [HttpPut("SignContract")]
    public async Task<IActionResult> SignContract([FromQuery] Guid singerId, Guid contractId, [FromServices] ContractCase contractCase)
    {//при наличии аутентификации - подписанта (singerId) берем из контекста запроса

        await contractCase.SignContract(singerId, contractId);

        return Ok();
    }

    [HttpPost("NotifyEmployee")]
    public async Task<IActionResult> NotifyEmployee([FromQuery] Guid employeeId, string messageSubject, [FromBody] string messageBody, [FromServices] EmailNotificationCase emailNotification)
    {
        emailNotification.PushMessageToEmployeeAsync(employeeId, messageSubject, messageBody);
        return Ok();
    }

}