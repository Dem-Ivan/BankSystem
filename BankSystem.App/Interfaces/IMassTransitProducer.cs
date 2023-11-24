using Contracts;

namespace BankSystem.App.Interfaces;
public interface IMassTransitProducer
{
    public Task SendMessageAsync<T>(T messageCommand) where T : IMessageCommand;
}
