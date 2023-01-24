using System;
using System.Collections.Generic;
using BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces
{
    public interface IClientRepository
    {       
        Client Get(Guid clientId);
        void Add(Client employee);
        void Update(Client employee);
        void Delete(Guid employeeId);
    }
}
