using System;
using BankSystem.App.DTO;
using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Exceptions;
using BankSystem.Domain.Models.Templates;
using AutoMapper;
using System.Linq;

namespace BankSystem.App.Cases
{
    public class ContractCase
    {
        private IClientRepository _clientRepository;
        private IEmployeeRepository _employeeRepository;
        private IContractRepository _contractRepository;
        private readonly IMapper _mapper;
        public ContractCase(IClientRepository clientRepository, IEmployeeRepository employeeRepository, IContractRepository contractRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _employeeRepository = employeeRepository;
            _contractRepository = contractRepository;
            _mapper = mapper;
        }

        //TODO: метод вызывается в контроллере с аутентификацией сотрудника
        public Guid CreateNewcontract(ContractTemplate template, Guid authorId, Guid counteragentId)
        {
            var author = _employeeRepository.Get(authorId);
            if (author == null)
            {
                throw new NotFoundException($"Сотруднк с идентификатором {authorId} не зарегистрирован в системе.");
            }

            var counteragent = _clientRepository.Get(counteragentId);
            if (counteragent == null)
            {
                throw new NotFoundException($"Клиент с идентификатором {counteragentId} не зарегистрирован в системе.");
            }

            var contract =  template.GetNewContract(author, counteragent);
            _contractRepository.Add(contract);
            _contractRepository.Save();

            return contract.Id;
        }

        public ContractResponse CompleteContract(Guid counteragentId, Guid contractId)
        {
            var counteragent = _clientRepository.Get(counteragentId);
            if (counteragent == null)
            {
                throw new NotFoundException($"Клиент с идентификатором {counteragentId} не зарегистрирован в системе.");
            }

            var contract = _contractRepository.Get(contractId);
            if (contract == null)
            {
                throw new NotFoundException($"Контракт с идентификатором {contractId} не зарегистрирован в системе.");
            }
            
            contract.Сomplete(counteragent);
            _contractRepository.AddContractHistoryElement(contract.History.LastOrDefault());
            contract.SendforAcquaintance();
            _contractRepository.AddContractHistoryElement(contract.History.LastOrDefault()); 
            _contractRepository.Save();     

            var result =  _mapper.Map<ContractResponse>(contract);    
            return result;
        }

        public Guid СonfirmAcquaintance(Guid counteragentId, Guid contractId)
        {
            var contract = _contractRepository.Get(contractId);
            if (contract == null)
            {
                throw new NotFoundException($"Контракт с идентификатором {contractId} не зарегистрирован в системе.");
            }

            var counteragent = _clientRepository.Get(counteragentId);
            if (counteragent == null)
            {
                throw new NotFoundException($"Клиент с идентификатором {counteragentId} не зарегистрирован в системе.");
            }

            contract.Cquaint(counteragent);
            _contractRepository.AddContractHistoryElement(contract.History.LastOrDefault());            
            _contractRepository.Save();

            return contract.Id;
        }

        public void SignContract(Guid signerId, Guid contractId)
        {
            var contract = _contractRepository.Get(contractId);
            if (contract == null)
            {
                throw new NotFoundException($"Контракт с идентификатором {contractId} не зарегистрирован в системе.");
            }

            var signer = _employeeRepository.Get(signerId);
            if (signer == null)
            {
                throw new NotFoundException($"Сотрудник с идентификатором {signerId} не зарегистрирован в системе.");
            }
           
            contract.Sign(signer);
            _contractRepository.AddContractHistoryElement(contract.History.LastOrDefault());
            _contractRepository.Save();
        }

        public void UpdateContractBody(Guid contractId, Guid redactorId, string newBody)
        {
            var contract = _contractRepository.Get(contractId);
            if (contract == null)
            {
                throw new NotFoundException($"Контракт с идентификатором {contractId} не зарегистрирован в системе.");
            }

            var redactor = _employeeRepository.Get(redactorId);
            if (redactor == null)
            {
                throw new NotFoundException($"Сотрудник с идентификатором {redactorId} не зарегистрирован в системе.");
            }

            if (contract.AuthorId != redactor.Id)
            {
                throw new InvalidAccessException($"Сотрудник {redactor.Name} не является автором кнтракта! Редактировать контракт может только его автор.");
            }

            contract.UpdateBody(newBody);           
            _contractRepository.Save();
        }
    }
}
