using System.IO;
using AutoMapper;
using BankSystem.API;
using BankSystem.API.Repositoryes;
using BankSystem.App.Cases;
using BankSystem.App.DTO;
using BankSystem.App.Mapping;
using BankSystem.Domain.Models;
using BankSystem.Domain.Models.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace BankSystem.Infrastructure.Test
{
    public class СontractLifecycleTest
    {
        private static ConfigurationBuilder _builder = new ConfigurationBuilder();
        private static IConfiguration _config = _builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@"appsettings.json").Build();
        private static DbContextOptionsBuilder<BankSystemDbContext> _optionsBuilder = new DbContextOptionsBuilder<BankSystemDbContext>();
        private static DbContextOptions<BankSystemDbContext> _options = _optionsBuilder.UseNpgsql(_config.GetConnectionString("DefaultConnection")).Options;

        // public static BankSystemDbContext _bankSystemDbContext = new BankSystemDbContext(_options);
        private static MapperConfiguration _mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile<MainProfile>(); });
        private static readonly IMapper _mapper = _mapperConfig.CreateMapper();


        [Fact]
        public void ContractRouteTest()
        {
            //Arrange
            using var bankSystemDbContext = new BankSystemDbContext(_options);
            var employeeRepository = new EmployeeRepository(bankSystemDbContext);
            var clientRepository = new ClientRepository(bankSystemDbContext);
            var contractRepository = new ContractRepository(bankSystemDbContext);
            var employeeCase = new RegisterEmployeeCase(employeeRepository, _mapper);
            var clientCase = new RegisterClientCase(clientRepository, _mapper);
            var contractCase = new ContractCase(clientRepository, employeeRepository, contractRepository, _mapper);


            var counteragent = new ClientRequest { Age = 22, Name = "Иван" };
            var bankOperator = new EmployeeRequest { Age = 33, Name = "Петрова", Role = role.ordinary_employee };
            var signer = new EmployeeRequest { Age = 45, Name = "Эдуард Степанович", Role = role.director };
            var template = ContractTemplate.GetInstance();
            template.SignerRole = role.director;


            var counteragentId = clientCase.AddClient(counteragent);
            var bankOperatorId = employeeCase.AddEmployee(bankOperator);
            var signerId = employeeCase.AddEmployee(signer);



            //Act
            var contractId = contractCase.CreateNewcontract(template, bankOperatorId, counteragentId); //1)
            var completedContract = contractCase.CompleteContract(counteragentId, contractId);//2)
            //фронт показывает тело контракта клиенту и ожидает нажатия на кнопку "Одобрить"
            //если кнопка "Одобрить" была нажата - вызываем метод СonfirmAcquaintance
            contractCase.СonfirmAcquaintance(counteragentId, contractId); //3)
            contractCase.SignContract(signerId, contractId);//4)

            //Assert
            //Assert.Equal(5, contract.History.Count);          
        }
    }
}
