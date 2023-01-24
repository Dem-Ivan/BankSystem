using AutoMapper;
using BankSystem.App.Cases;
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
        private readonly IMapper _mapper;// TODO: пробросить карту

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

            var contractCase = new ContractCase(_clientRepository, _employeeRepository, _contractRepository, _mapper);//TODO: самое время добавить рабту со стабами
                        
            //Act
            var contract = _template.GetNewContract();
            
            //Assert
        }
    }
}
