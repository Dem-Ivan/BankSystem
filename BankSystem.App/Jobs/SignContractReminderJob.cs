using BankSystem.App.Interfaces;
using Contracts;
using Quartz;

namespace BankSystem.App.Jobs;
public class SignContractReminderJob : IJob
{
    private IMassTransitProducer _massTransitProducer;
    private IUnitOfWork _unitOfWork;

    public SignContractReminderJob(IRabbitProducer rabbitProducer,
        IMassTransitProducer massTransitProducer,
        IUnitOfWork unitOfWork)
    {        
        _massTransitProducer = massTransitProducer ?? throw new ArgumentNullException(nameof(massTransitProducer));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var unSignedContracts = await _unitOfWork.Contracts.GetUnSignedContracts();
        if (unSignedContracts.Any())
        {
            foreach (var contract in unSignedContracts)
            {
                var director = await _unitOfWork.Employees.GetAsync(contract.SignerRole);
                if (director is not null)
                {
                    var messageSubject = "Напоминание!";
                    var messageBody = $"Уважаемый {director.Name} у вас на рассмотрении находится контракт №{contract.Number}!";

                    await Send(director.Email, messageSubject, messageBody);
                }
            }            
        }
    }

    private async Task Send(string email, string messageSubject, string messageBody)
    {
        var emailMessage = new EmailMessageCommand
        {
            Email = email,
            Heading = messageSubject,
            MessageText = messageBody
        };

        await _massTransitProducer.SendMessageAsync(emailMessage);
    }
}
