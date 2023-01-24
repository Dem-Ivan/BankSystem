using System;
using System.Collections.Generic;
using BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces
{
    public interface IContractRepository
    {       
        Contract Get(Guid contractId);
        void Add(Contract contract);
        void Update(Contract contract);
        void Delete(Guid contractId);
    }
}
