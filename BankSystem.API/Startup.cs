using BankSystem.API.Options;
using BankSystem.API.Producers;
using BankSystem.API.QuatzExtensions;
using BankSystem.API.Repositories;
using BankSystem.App.Cases;
using BankSystem.App.Interfaces;
using BankSystem.App.Mapping;
using BankSystem.Domain.Validators;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BankSystem.API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
  
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();       
        services.AddAutoMapper(typeof(MainProfile));
        services.Configure<RabbitOptions>(c => Configuration.GetSection(nameof(RabbitOptions)).Bind(c));

        services.AddScoped<RegisterEmployeeCase>();
        services.AddScoped<RegisterClientCase>();
        services.AddScoped<ContractCase>();
        services.AddScoped<NotificationCase>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IContractRepository, ContractRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRabbitProducer, RabbitProducer>();       
        services.AddScoped<IMassTransitProducer, MassTransitProducer>();        
        services.AddScoped<ClientValidator>();
        services.AddScoped<EmployeeValidator>();               
       
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "BankSystem.API", Version = "v1" });
        });
       
        services.AddDbContext<BankSystemDbContext>(builder =>
        {
            builder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"), optionBuilder =>
            {
                optionBuilder.MigrationsAssembly(typeof(BankSystemDbContext).Assembly.GetName().Name);
            });
        });

        services.AddMassTransit(x =>
        {            
            x.UsingRabbitMq((context, cfg) =>
            {               
                cfg.ConfigureEndpoints(context);
            });
        });

        services.QuartzJobsRegistering(Configuration);//TODO
    }
       
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BankSystem.API v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}