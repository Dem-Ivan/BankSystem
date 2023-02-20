using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models;
using FluentValidation;

namespace BankSystem.Domain.Validators;
public static class ContractValidatorExtension
{
    //public IRuleBuilderOptions<Contract, Status>  StatusValidation(this IRuleBuilderOptions<Contract, Status> ruleBuilderOptions, Status expectedStatus)
    //{
    //    var f =  ruleBuilderOptions.Equal(expectedStatus).WithMessage($"Для выполнения данной операии контракт должен обладать статусом {expectedStatus}.");
    //}
}
