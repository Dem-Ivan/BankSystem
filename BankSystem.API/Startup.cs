using System.Text.Json.Serialization;
using BankSystem.API.Repositoryes;
using BankSystem.App.Cases;
using BankSystem.App.Interfaces;
using BankSystem.App.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BankSystem.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BankSystem.API", Version = "v1" });
            });
            services.AddAutoMapper(typeof(MainProfile));
            services.AddScoped<RegisterEmployeeCase>();
            services.AddScoped<RegisterClientCase>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>(); 
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ContractCase>();
            services.AddDbContext<BankSystemDbContext>(builder => 
            {
                builder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"), optionBuilder =>
                {
                    optionBuilder.MigrationsAssembly(typeof(BankSystemDbContext).Assembly.GetName().Name);
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
}
