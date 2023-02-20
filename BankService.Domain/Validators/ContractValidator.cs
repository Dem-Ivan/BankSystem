using BankSystem.Domain.Models;
using FluentValidation;

namespace BankSystem.Domain.Validators;
public class ContractValidator : AbstractValidator<Contract>
{    
    public ContractValidator(Status expectedStatus)
    {
        RuleFor(con => con.Status)
            .Equal(expectedStatus)
            .WithMessage($"Для выполнения данной операии контракт должен обладать статусом {expectedStatus}.");

        RuleFor(con => con.Body).Must(isContineClientName).When(con => con.Status == Status.ForAcquaintance)
            .WithMessage("Подтвердить факт ознакомления с контрактом " +
                         "может только пользователь с которым контракт заключается!");
    }

    private bool isContineClientName(string body)
    {
        var name = " "; //TODO: недоделал
        //return body.Contains(" ");
        return true;
    }
}
