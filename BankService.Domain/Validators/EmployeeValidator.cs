using BankSystem.Domain.Models;
using FluentValidation;

namespace BankSystem.Domain.Validators;
public class EmployeeValidator : AbstractValidator<Employee>
{
    public EmployeeValidator()
    {
        RuleFor(e => e.Name)
           .NotNull()
           .NotEmpty()
           .WithMessage("Имя сотрудника обязательно.");
        RuleFor(e => e.Age)
            .GreaterThan(18).WithMessage("Минимальный возраст сотрудника равен 18 годам.")
            .LessThan(50).WithMessage("Максимальниый возраст сотрудника равен 50 годам.");
        RuleFor(e => e.Email)
           .NotNull()
           .NotEmpty();
    }
}
