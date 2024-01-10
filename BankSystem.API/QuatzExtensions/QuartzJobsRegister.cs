using BankSystem.App.Jobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace BankSystem.API.QuatzExtensions;
public static class QuartzJobsRegister
{
    public static IServiceCollection QuartzJobsRegistering(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            q.AddJobAndTrigger<SignContractReminderJob>(configuration);
        }).AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        
        return services;
    }
}
