using AutoMapper;
using BankSystem.App.Cases;
using BankSystem.App.Mapping;
using BankSystem.App.Tests.Stubs;
using BankSystem.Domain.Models;
using BankSystem.Domain.Models.Templates;
using Xunit;

namespace BankSystem.App.Tests;

public class ContractCaseTest
{
    private ClientRepositoryStub _clientRepository = new();
    private EmployeeRepositoryStub _employeeRepository = new();
    private ContractRepositoryStub _contractRepository = new();
    private UnitOfWorkStub _unitOfWork;

    private static MapperConfiguration _mapperConfig = new(cfg => { cfg.AddProfile<MainProfile>(); });
    private readonly IMapper _mapper = _mapperConfig.CreateMapper();

    [Fact]
    public async Task FullCaseCheck()
    {
        //Arrange
        var template = ContractTemplate.GetInstance();
        var counteragent = new Client { Age = 22, Name = "Иван" };
        var bankOperator = new Employee(33, "Петрова", Role.OrdinaryEmployee);
        var signer = new Employee(45, "Эдуард Степанович", Role.Director);

        await _clientRepository.AddAsync(counteragent);
        await _employeeRepository.AddAsync(bankOperator);
        await _employeeRepository.AddAsync(signer);
        _unitOfWork = new UnitOfWorkStub(_employeeRepository, _clientRepository,_contractRepository);
        template.SignerRole = Role.Director;
        
        var contractCase = new ContractCase(_unitOfWork, _mapper);

        //Act
        var contractId = await contractCase.CreateNewcontract(template, bankOperator.Id, counteragent.Id); //1)
        var completedContract = await contractCase.CompleteContract(counteragent.Id, contractId);//2)
        //фронт показывает тело контракта клиенту и ожидает нажатия на кнопку "Одобрить"
        //если кнопка "Одобрить" была нажата - вызываем метод СonfirmAcquaintance
        await contractCase.СonfirmAcquaintance(counteragent.Id, contractId); //3)
        await contractCase.SignContract(signer.Id, contractId);//4)

        var contract = await _unitOfWork.Contracts.GetAsync(c => c.Id == contractId);
        //Assert
        Assert.Equal(5, contract.History.Count);
    }
}