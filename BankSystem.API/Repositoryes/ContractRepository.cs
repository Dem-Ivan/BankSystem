using System;
using System.Collections.Generic;
using System.Linq;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

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
            return _bankSystemDbContext.Contract.Include(c => c.History).FirstOrDefault(x => x.Id == contractId);
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
