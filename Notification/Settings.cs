using Contracts;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Consumers;
using Notification.Options;

namespace Notification;
internal class Settings
{
    private static ConfigurationBuilder _builder = new();
    private static IConfiguration _config = _builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@"configuration.json").Build();

    public IServiceCollection ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<RabbitEmailConsumer>();
        services.Configure<EmailOptions>(c => _config.GetSection(nameof(EmailOptions)).Bind(c));
        services.Configure<RabbitOptions>(c => _config.GetSection(nameof(RabbitOptions)).Bind(c));

        services.AddMassTransit(x =>
        {
            x.AddConsumer<MassTransitEmailConsumer>().Endpoint(x => x.Name = nameof(EmailMessageCommand));
            x.UsingRabbitMq((context, cfg) =>
            {        
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
