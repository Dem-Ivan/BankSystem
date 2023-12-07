using Microsoft.Extensions.Configuration;
using Quartz;

namespace BankSystem.API.QuatzExtensions;
public static class ServiceCollectionQuartzConfiguratorExtensions
{
    public static void AddJobAndTrigger<T>(this IServiceCollectionQuartzConfigurator quartzConfigurator, IConfiguration config) where T : IJob
    {
       
        string jobName = typeof(T).Name;
        var configKey = $"Quartz:{jobName}:Cron";     
        var cronSchedule = config[configKey];
        
        if (string.IsNullOrEmpty(cronSchedule))
        {
            throw new Exception($"Не найдены настройки конфигурации планировщика Quartz.NET Cron с ключем {configKey}");
        }

        var jobKey = new JobKey(jobName);
        quartzConfigurator.AddJob<T>(c => c.WithIdentity(jobKey));

        quartzConfigurator.AddTrigger(tc => tc
        .ForJob(jobKey)
        .WithIdentity(jobName + "-trigger")
        .WithCronSchedule(cronSchedule));
    }
}
