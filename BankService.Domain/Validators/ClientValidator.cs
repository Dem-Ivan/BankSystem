using BankSystem.Domain.Models;
using FluentValidation;

namespace BankSystem.Domain.Validators;
public class ClientValidator : AbstractValidator<Client>
{
    public ClientValidator()
    {
        RuleFor(c => c.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Имя клиента обязательно.");
        RuleFor(c => c.Age)
            .GreaterThan(18).WithMessage("Минимальный возраст клиента равен 18 годам.")
            .LessThan(100).WithMessage("Максимальниый возраст клиента равен 100 годам.");
        RuleFor(c => c.Email)
          .NotNull()
          .NotEmpty();
    }
}
