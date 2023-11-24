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

                cfg.PrefetchCount = 3;// Количество сообщений, которые будут получены за раз
                cfg.ConcurrentMessageLimit = 2;// Количество сообщений, которые будут обрабатываться одновременно
                
                cfg.UseMessageRetry(r =>
                {                    
                    r.Handle<RequestTimeoutException>();//если тип ошибки 408 отрабатывает повторная отаравка по следующей настройке
                    r.Interval(1, TimeSpan.FromSeconds(2));//Настройка повторной обработки сообщений 3 раза с интервалом 2 сек если тип ошибки 408                                   
                });                 
            });
        });

        return services;
    }
}
