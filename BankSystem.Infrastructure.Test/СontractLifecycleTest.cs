using AutoMapper;
using BankSystem.API;
using BankSystem.API.Repositoryes;
using BankSystem.App.Cases;
using BankSystem.App.DTO;
using BankSystem.App.Mapping;
using BankSystem.Domain.Models;
using BankSystem.Domain.Models.Templates;
using Xunit;

namespace BankSystem.Infrastructure.Test
{
    public class СontractLifecycleTest
    {
        private static BankSystemDbContext _bankSystemDbContext = new BankSystemDbContext();
        private static MapperConfiguration _mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile<MainProfile>(); });
        private static readonly IMapper _mapper = _mapperConfig.CreateMapper();
        private static EmployeeRepository _employeeRepository = new EmployeeRepository(_bankSystemDbContext);
        private static ClientRepository _clientRepository = new ClientRepository(_bankSystemDbContext);
        private static ContractRepository _contractRepository = new ContractRepository(_bankSystemDbContext);
        private RegisterEmployeeCase _employeeCase = new RegisterEmployeeCase(_employeeRepository, _mapper);
        private RegisterClientCase _clientCase = new RegisterClientCase(_clientRepository, _mapper);
        private ContractCase _contractCase = new ContractCase(_clientRepository, _employeeRepository, _contractRepository,_mapper );

        [Fact]
        public void ContractRouteTest()
        {
            //Arrange
            var counteragent = new ClientRequest { Age = 22, Name = "Иван" };
            var bankOperator = new EmployeeRequest { Age = 33, Name = "Петрова", Role = role.ordinary_employee };
            var signer = new EmployeeRequest { Age = 45, Name = "Эдуард Степанович", Role = role.director};
            var template = ContractTemplate.GetInstance();
            template.SignerRole = role.director;


            var counteragentId  = _clientCase.AddClient(counteragent);            
            var bankOperatorId = _employeeCase.AddEmployee(bankOperator);
            var signerId = _employeeCase.AddEmployee(signer);



            //Act
            var contractId = _contractCase.CreateNewcontract(template, bankOperatorId); //1)
            var completedContract = _contractCase.CompleteContract(counteragentId, contractId);//2)
            //фронт показывает тело контракта клиенту и ожидает нажатия на кнопку "Одобрить"
            //если кнопка "Одобрить" была нажата - вызываем метод СonfirmAcquaintance
            _contractCase.СonfirmAcquaintance(counteragentId, contractId); //3)
            _contractCase.SignContract(signerId, contractId);//4)

            //Assert
            //Assert.Equal(5, contract.History.Count);          
        }
    }
}
