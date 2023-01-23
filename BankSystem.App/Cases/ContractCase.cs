using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using BankSystem.Domain.Models.Templates;

namespace BankSystem.App.Cases
{
    public class ContractCase
    {
        private IClientRepository _clientRepository;
        private IEmployeeRepository _employeeRepository;

        public ContractCase(IClientRepository clientRepository, IEmployeeRepository employeeRepository)
        {
            _clientRepository = clientRepository;
            _employeeRepository = employeeRepository;
        }
        //TODO: метод вызывается в контроллере с аутентификацией сотрудника
        public Contract CreateNewcontract(ContractTemplate template)
        {
            return template.GetNewContract();
        }

        public Contract CompleteContract(Guid counteragentId, Contract contract)
        {
            var counteragent = _clientRepository.GetClient(counteragentId);
            if (counteragent == null)
            {
                throw new NotFoundException($"Клиент с идентификатором {counteragentId} не зарегистрирован в системе.");
            }

            contract.Сomplete(counteragent);
            return contract;
        }

        public Contract SendContractForClient(Guid counteragentId, Contract contract)
        {
            var counteragent = _clientRepository.GetClient(counteragentId);
            if (counteragent == null)
            {
                throw new NotFoundException($"Клиент с идентификатором {counteragentId} не зарегистрирован в системе.");
            }

            contract.SendforAcquaintance(counteragent);
            //делавем запрос клиенту на одобрение контракта, отправляем контракт на ознакомление
            //если пользователь одобрил меняем статус
            contract.Cquaint(counteragent);
            return contract;
        }

        public Contract SendForSigner(Guid signerId, Contract contract)
        {
            var signer = _employeeRepository.GetEmployee(signerId);
            if (signer == null)
            {
                throw new NotFoundException($"Сотрудник с идентификатором {signerId} не зарегистрирован в системе.");
            }

            contract.Sign(signer);
            return contract;
        }
    }
}
