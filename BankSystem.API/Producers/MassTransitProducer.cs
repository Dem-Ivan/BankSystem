using BankSystem.App.Interfaces;
using Contracts;
using MassTransit;

namespace BankSystem.API.Producers;
public class MassTransitProducer : IMassTransitProducer
{
    private readonly ISendEndpointProvider _sendEndpointProvider;
    public MassTransitProducer(ISendEndpointProvider sendEndpointProvider)
    {
        _sendEndpointProvider = sendEndpointProvider ?? throw new ArgumentNullException(nameof(sendEndpointProvider));
    }

    public async Task SendMessageAsync<T>(T messageCommand) where T : IMessageCommand
    {
        var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"exchange:{nameof(messageCommand)}"));
       
        messageCommand.RequestId = NewId.NextGuid();
        messageCommand.TimeToProcessing = GetTimeToProcessing();

        await endpoint.Send(messageCommand);
    }

    private int GetTimeToProcessing()
    {
        var rand = new Random();
        return rand.Next(1000, 10000);
    }
}
