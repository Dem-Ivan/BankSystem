using System;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.App.Tests
{
    public class ClientRepositoryStub : IClientRepository
    {
        private Client _client;

        public void Add(Client client)
        {
            _client = client;
        }

        public void Delete(Guid clientId)
        {
            throw new NotImplementedException();
        }

        public Client Get(Guid clientId)
        {            
            return _client;
        }

        public void Update(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
