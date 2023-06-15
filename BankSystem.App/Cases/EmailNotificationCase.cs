using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;
using Contracts;

namespace BankSystem.App.Cases;
public class EmailNotificationCase
{
    private IRabbitProducer _rabbitProducer;
    private IUnitOfWork _unitOfWork;

    public EmailNotificationCase(IRabbitProducer rabbitProducer, IUnitOfWork unitOfWork)
    {
        _rabbitProducer = rabbitProducer ?? throw new ArgumentNullException(nameof(rabbitProducer));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task PushMessageToEmployeeAsync(Guid employeeId, string messageSubject, string messageBody)
    {
        var employee = await _unitOfWork.Employees.GetAsync(employeeId);
        if (employee is null)
        {
            throw new NotFoundException($"Сотрудник с идентификатором {employeeId} не зарегистрирован в системе.");
        }

        var emailMessage = new EmailMessageCommand
        {
            RequestId = Guid.NewGuid(),
            Email =employee.Email,
            Heading = messageSubject,
            MessageText = messageBody
        };

        _rabbitProducer.SendMessage(emailMessage);
    }
}
