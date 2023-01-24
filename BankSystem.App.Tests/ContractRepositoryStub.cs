using System;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.App.Tests
{
    public class ContractRepositoryStub : IContractRepository
    {
        private Contract _contract;
        public void Add(Contract contract)
        {
            _contract = contract;
        }

        public void Delete(Guid contractId)
        {
            throw new NotImplementedException();
        }

        public Contract Get(Guid contractId)
        {
            return _contract;
        }

        public void Update(Contract contract)
        {
            throw new NotImplementedException();
        }
    }
}
