using Contracts;
using MassTransit;

namespace Notification.Consumers;
internal class MassTransitEmailConsumer : IConsumer<EmailMessageCommand>
{
    private IEmailSender _emailSender;

    public MassTransitEmailConsumer(IEmailSender emailSender)
    {
        _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
    }

    public async Task Consume(ConsumeContext<EmailMessageCommand> context)
    {            
        Console.WriteLine($"Потребитель {nameof(MassTransitEmailConsumer)} получил команду на отправку сообщения.\n" +
            $"Идентификатор сообщения {context.Message.RequestId} TimeToProcessing = {context.Message.TimeToProcessing}");

        Thread.Sleep(context.Message.TimeToProcessing);
        await _emailSender.SendAsync(context.Message.Heading, context.Message.MessageText, context.Message.Email);

        Console.WriteLine($"Потребитель {nameof(MassTransitEmailConsumer)} отправил письмо с текстом: {context.Message.MessageText} на адрес {context.Message.Email}");
    }
}
