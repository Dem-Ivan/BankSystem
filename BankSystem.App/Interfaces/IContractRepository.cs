using System;
using System.Collections.Generic;
using BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces
{
    public interface IContractRepository
    {       
        Contract Get(Guid contractId);
        IEnumerable<ContractHistoryElement> GetContractHistory();
        void Add(Contract contract);
        void AddContractHistoryElement(ContractHistoryElement contractHistoryElement);
        void Update(Contract contract);
        void Delete(Guid contractId);
        public void Save();
    }
}
