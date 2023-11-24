using BankSystem.API.Options;
using BankSystem.App.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace BankSystem.API.Producers;
internal class RabbitProducer : IRabbitProducer
{
    private RabbitOptions _options;

    public RabbitProducer(IOptions<RabbitOptions> options)
    {
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public void SendMessage<T>(T messageContract) where T : class
    {
        var message = JsonSerializer.Serialize(messageContract);
        SendMessage(message);
    }

    private void SendMessage(string message)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _options.HostName,            
        };

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.ExchangeDeclare(exchange: _options.ExchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queue: _options.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind(_options.QueueName, _options.ExchangeName, _options.RoutingKey, arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: _options.ExchangeName, routingKey: _options.RoutingKey, basicProperties: null, body: body);
        }
    }
}
