using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Notification;
using Notification.Consumers;

Console.WriteLine("Консюмер запущен");

var settings = new Settings();
var services = settings.ConfigureServices();
var serviceProvider = services.BuildServiceProvider();

var cts = new CancellationTokenSource();
var token = cts.Token;

//for RabbitMQ
var rabbitMqListener = serviceProvider.GetService<RabbitEmailConsumer>();
await rabbitMqListener.ExsecutAsync(token);

//For MassTransit 
var busControl = serviceProvider.GetRequiredService<IBusControl>();
await busControl.StartAsync();


Console.ReadLine(); 