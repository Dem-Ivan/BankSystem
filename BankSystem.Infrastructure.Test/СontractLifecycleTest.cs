using System.IO;
using AutoMapper;
using BankSystem.API;
using BankSystem.API.Repositories;
using BankSystem.App.Cases;
using BankSystem.App.DTO;
using BankSystem.App.Mapping;
using BankSystem.Domain.Models;
using BankSystem.Domain.Models.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BankSystem.Infrastructure.Test;

public class СontractLifecycleTest
{
    private static ConfigurationBuilder _builder = new();
    private static IConfiguration _config = _builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@"appsettings.json").Build();
    private static DbContextOptionsBuilder<BankSystemDbContext> _optionsBuilder = new();
    private static DbContextOptions<BankSystemDbContext> _options = _optionsBuilder.UseNpgsql(_config.GetConnectionString("DefaultConnection")).Options;

    private static MapperConfiguration _mapperConfig = new(cfg => { cfg.AddProfile<MainProfile>(); });
    private static readonly IMapper _mapper = _mapperConfig.CreateMapper();

    [Fact]
    public void ContractRouteTest()
    {
        //Arrange
        using var bankSystemDbContext = new BankSystemDbContext(_options);
        var uow = new UnitOfWork(bankSystemDbContext);
        var employeeCase = new RegisterEmployeeCase(uow, _mapper);
        var clientCase = new RegisterClientCase(uow, _mapper);
        var contractCase = new ContractCase(uow, _mapper);


        var counteragent = new ClientRequest { Age = 22, Name = "Иван" };
        var bankOperator = new EmployeeRequest { Age = 33, Name = "Петрова", Role = Role.OrdinaryEmployee };
        var signer = new EmployeeRequest { Age = 45, Name = "Эдуард Степанович", Role = Role.Director };
        var template = ContractTemplate.GetInstance();
        template.SignerRole = Role.Director;


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