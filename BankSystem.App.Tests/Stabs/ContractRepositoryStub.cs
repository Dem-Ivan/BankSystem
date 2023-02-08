using System;
using System.Collections.Generic;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.App.Tests.Stabs
{
    public class ContractRepositoryStub : IContractRepository
    {
        private Contract _contract;
        public void Add(Contract contract)
        {
            _contract = contract;
        }

        public void AddContractHistoryElement(ContractHistoryElement contractHistoryElement)
        {
           
        }

        public void Delete(Guid contractId)
        {
            throw new NotImplementedException();
        }

        public Contract Get(Guid contractId)
        {
            return _contract;
        }

        public IEnumerable<ContractHistoryElement> GetContractHistory()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            
        }

        public void Update(Contract contract)
        {
            _contract = contract;
        }
    }
}
