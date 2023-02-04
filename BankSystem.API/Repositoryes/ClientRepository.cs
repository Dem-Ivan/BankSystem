using System;
using System.Linq;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.API.Repositoryes
{
    public class ClientRepository : IClientRepository
    {

        private BankSystemDbContext _bankSystemDbContext;

        public ClientRepository(BankSystemDbContext bankSystemDbContext)
        {
            _bankSystemDbContext = bankSystemDbContext;
        }

        public Client Get(Guid clientId)
        {
            return _bankSystemDbContext.Client.FirstOrDefault(x => x.Id == clientId);
        }

        public void Add(Client client)
        {
            _bankSystemDbContext.Client.Add(client);
        }

        public void Delete(Guid clientId)
        {
            var client = _bankSystemDbContext.Client.FirstOrDefault(c => c.Id == clientId);
            _bankSystemDbContext.Client.Remove(client);
        }      

        public void Update(Client client)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _bankSystemDbContext.SaveChanges();
        }
    }
}
