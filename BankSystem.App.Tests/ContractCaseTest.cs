﻿using AutoMapper;
using BankSystem.App.Cases;
using BankSystem.App.Mapping;
using BankSystem.App.Tests.Stabs;
using BankSystem.Domain.Models;
using BankSystem.Domain.Models.Templates;
using Xunit;

namespace BankSystem.App.Tests
{
    public class ContractCaseTest
    {
        ContractTemplate _template = ContractTemplate.GetInstance();
        private ClientRepositoryStub _clientRepository = new ClientRepositoryStub();
        private EmployeeRepositoryStub _employeeRepository = new EmployeeRepositoryStub();
        private ContractRepositoryStub _contractRepository = new ContractRepositoryStub();

        private static MapperConfiguration _mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile<MainProfile>(); });
        private readonly IMapper _mapper = _mapperConfig.CreateMapper();
        

        [Fact]
        public void FullCaseCheck()
        {
            //Arrange            
            var counteragent = new Client(22, "Иван");
            var bankOperator = new Employee(33, "Петрова", role.ordinary_employee);
            var signer = new Employee(45, "Эдуард Степанович", role.director);

            _clientRepository.Add(counteragent);
            _employeeRepository.Add(bankOperator);
            _employeeRepository.Add(signer);
            _template.SignerRole = role.director;

            var contractCase = new ContractCase(_clientRepository, _employeeRepository, _contractRepository, _mapper);

            //Act
            var contractId = contractCase.CreateNewcontract(_template, bankOperator); //1)
            var completedContract = contractCase.CompleteContract(counteragent.Id, contractId);//2)
            //фронт показывает тело контракта клиенту и ожидает нажатия на кнопку "Одобрить"
            //если кнопка "Одобрить" была надата - вызываем метод СonfirmAcquaintance
            contractCase.СonfirmAcquaintance(counteragent.Id, contractId); //3)
            contractCase.SignContract(signer.Id, contractId);//4)
            
            //Assert
        }
    }
}