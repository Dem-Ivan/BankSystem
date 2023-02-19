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
    public async Task ContractRouteTest()
    {
        //Arrange
        
        using var bankSystemDbContext = new BankSystemDbContext(_options);
        var employeeRepository = new EmployeeRepository(bankSystemDbContext);
        var clientRepository = new ClientRepository(bankSystemDbContext);       
        var contractRepository = new ContractRepository(bankSystemDbContext);
        var uow = new UnitOfWork(employeeRepository, clientRepository, contractRepository, bankSystemDbContext);
        var employeeCase = new RegisterEmployeeCase(uow, _mapper);
        var clientCase = new RegisterClientCase(uow, _mapper);
        var contractCase = new ContractCase(uow, _mapper);

        var counteragent = new ClientRequest { Age = 22, Name = "Иван", Email = "@email" };
        var bankOperator = new EmployeeRequest { Age = 33, Name = "Петрова", Email = "@email", Role = Role.OrdinaryEmployee };
        var signer = new EmployeeRequest { Age = 45, Name = "Эдуард Степанович", Email = "@email", Role = Role.Director };
        var template = ContractTemplate.GetInstance();
        template.SignerRole = Role.Director;

        var counteragentId = await clientCase.AddClient(counteragent);
        var bankOperatorId = await employeeCase.AddEmployee(bankOperator);
        var signerId = await employeeCase.AddEmployee(signer);

        //Act
        var contractId = await contractCase.CreateNewcontract(template, bankOperatorId, counteragentId); //1)
        var completedContract = await contractCase.CompleteContract(counteragentId, contractId);//2)
        //фронт показывает тело контракта клиенту и ожидает нажатия на кнопку "Одобрить"
        //если кнопка "Одобрить" была нажата - вызываем метод СonfirmAcquaintance
        await contractCase.СonfirmAcquaintance(counteragentId, contractId); //3)
        await contractCase.SignContract(signerId, contractId);//4)
        var contract = await uow.Contracts.GetAsync(c => c.Id == contractId);

        //Assert
        Assert.Equal(5, contract.History.Count);
    }
}