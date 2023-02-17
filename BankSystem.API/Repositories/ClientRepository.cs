using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.API.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly BankSystemDbContext _bankSystemDbContext;

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
}