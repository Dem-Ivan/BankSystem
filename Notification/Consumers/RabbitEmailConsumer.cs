using System.Text;
using Contracts;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Notification.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Notification.Consumers;
internal class RabbitEmailConsumer : IDisposable
{
    private IConnection _connection;
    private IModel _channel;
    private IEmailSender _emailSender;
    private readonly RabbitOptions _rabbitoptions;
    public RabbitEmailConsumer(IOptions<RabbitOptions> rabbitoptions, IEmailSender emailSender)
    {
        _rabbitoptions = rabbitoptions.Value ?? throw new ArgumentNullException(nameof(rabbitoptions));
        _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));

        var factory = new ConnectionFactory { HostName = _rabbitoptions.HostName };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: _rabbitoptions.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
       
    }

    public async Task ExsecutAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (_, eventArgs) =>
        {
            var content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            var deserializedMessage = JsonConvert.DeserializeObject<EmailMessageCommand>(content);

            Console.WriteLine($"Потребитель {nameof(RabbitEmailConsumer)} получил команду на отправку сообщения");

            await _emailSender.SendAsync(deserializedMessage.Heading, deserializedMessage.MessageText, deserializedMessage.Email, stoppingToken);

            Console.WriteLine($"Потребитель {nameof(RabbitEmailConsumer)} отрпавил письмо с текстом: {deserializedMessage.MessageText} на адрес {deserializedMessage.Email}");

            _channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        _channel.BasicConsume(_rabbitoptions.QueueName, false, consumer);
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}
