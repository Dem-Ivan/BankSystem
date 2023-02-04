using System;
using BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces
{
    public interface IClientRepository
    {       
        Client Get(Guid clientId);
        void Add(Client client);
        void Update(Client client);
        void Delete(Guid clientId);
        void Save();
    }
}
