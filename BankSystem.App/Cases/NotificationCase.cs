using System.Threading.Tasks;
using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using Contracts;

namespace BankSystem.App.Cases;
public class NotificationCase
{
    private IRabbitProducer _rabbitProducer;
    IMassTransitProducer _massTransitProducer;
    private IUnitOfWork _unitOfWork;

    public NotificationCase(IRabbitProducer rabbitProducer, IMassTransitProducer massTransitProducer, IUnitOfWork unitOfWork)
    {
        _rabbitProducer = rabbitProducer ?? throw new ArgumentNullException(nameof(rabbitProducer));
        _massTransitProducer = massTransitProducer ?? throw new ArgumentNullException(nameof(massTransitProducer));
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
            Email = employee.Email,
            Heading = messageSubject,
            MessageText = messageBody
        };

        _rabbitProducer.SendMessage(emailMessage);
    }

    public async Task PushMessagesToClientsAsync(Guid[] clientsId, string messageSubject, string messageBody)
    {
        foreach (var clientId in clientsId) 
        {
            var client = await _unitOfWork.Clients.GetAsync(clientId);
            if (client is null)
            {
                throw new NotFoundException($"Клиент с идентификатором {clientId} не зарегистрирован в системе.");
            }

            var emailMessage = new EmailMessageCommand
            {
                Email = client.Email,
                Heading = messageSubject,
                MessageText = messageBody
            };

            await _massTransitProducer.SendMessageAsync(emailMessage);
        }
    }
}
