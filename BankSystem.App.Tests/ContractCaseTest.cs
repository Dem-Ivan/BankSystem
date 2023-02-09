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

    //private static MapperConfiguration _mapperConfig = new MapperConfiguration(cfg =>
    //{ cfg.CreateMap<Contract, ContractResponse>(MemberList.Source); });

    private static MapperConfiguration _mapperConfig = new(cfg => { cfg.AddProfile<MainProfile>(); });
    private readonly IMapper _mapper = _mapperConfig.CreateMapper();

    [Fact]
    public void FullCaseCheck()
    {
        //Arrange
        var template = ContractTemplate.GetInstance();
        var counteragent = new Client { Age = 22, Name = "Иван" };
        var bankOperator = new Employee(33, "Петрова", Role.OrdinaryEmployee);
        var signer = new Employee(45, "Эдуард Степанович", Role.Director);

        _clientRepository.Add(counteragent);
        _employeeRepository.Add(bankOperator);
        _employeeRepository.Add(signer);
        template.SignerRole = Role.Director;

        //TODO
        ContractCase contractCase = default; //= new ContractCase(_clientRepository, _employeeRepository, _contractRepository, _mapper);

        //Act
        var contractId = contractCase.CreateNewcontract(template, bankOperator.Id, counteragent.Id); //1)
        var completedContract = contractCase.CompleteContract(counteragent.Id, contractId);//2)
        //фронт показывает тело контракта клиенту и ожидает нажатия на кнопку "Одобрить"
        //если кнопка "Одобрить" была нажата - вызываем метод СonfirmAcquaintance
        contractCase.СonfirmAcquaintance(counteragent.Id, contractId); //3)
        contractCase.SignContract(signer.Id, contractId);//4)

        //Assert
    }
}