using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.API.Repositoryes
{
    public class ContractRepository : IContractRepository
    {
        private BankSystemDbContext _bankSystemDbContext;

        public ContractRepository(BankSystemDbContext bankSystemDbContext)
        {
            _bankSystemDbContext = bankSystemDbContext;
        }

        public Contract Get(Guid contractId)
        {
            return _bankSystemDbContext.Contract.FirstOrDefault(x => x.Id == contractId);
        }

        public void Add(Contract contract)
        {
            _bankSystemDbContext.Contract.Add(contract);
        }

        public void Delete(Guid contractId)
        {
            var contract = _bankSystemDbContext.Contract.FirstOrDefault(c => c.Id == contractId);
            _bankSystemDbContext.Contract.Remove(contract);
        }

        public void Update(Contract contract)
        {
           var oldContract = _bankSystemDbContext.Contract.FirstOrDefault(x => x.Id == contract.Id);

            oldContract = contract; // TODO: проверить, возможно понадобится маппинг, не забыть про вызов метода сохранения
        }

        public IEnumerable<ContractHistoryElement> GetContractHistory()
        {
           return _bankSystemDbContext.ContractHistory;
        }

        public void AddContractHistoryElement(ContractHistoryElement contractHistoryElement)
        {
            _bankSystemDbContext.ContractHistory.Add(contractHistoryElement);
        }

        public void Save()
        {
            _bankSystemDbContext.SaveChanges();
        }
    }
}
