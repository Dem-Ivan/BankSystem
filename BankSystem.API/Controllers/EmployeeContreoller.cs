using BankSystem.App.Cases;
using BankSystem.App.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeContreoller : ControllerBase
    {
        RegisterEmployeeCase _employeeCase;

        public EmployeeContreoller(RegisterEmployeeCase employeeCase)
        {
            _employeeCase = employeeCase;
        }


        [HttpGet]
        public ActionResult<EmployeeResponse> GetEmployee([FromQuery] Guid employeeId)
        {            
            return _employeeCase.Get(employeeId);
        }

        [HttpPost]
        public ActionResult<Guid> AddEmployee([FromBody] EmployeeRequest employeeReq)
        {
            return _employeeCase.AddEmploye(employeeReq);
        }

    }
}
