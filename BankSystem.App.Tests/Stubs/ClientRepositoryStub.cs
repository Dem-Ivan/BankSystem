using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.App.Tests.Stubs;

public class ClientRepositoryStub : IClientRepository
{
    private Client _client;

    public void Add(Client client)
    {
        _client = client;
    }    

    public Client Get(Guid clientId)
    {
        return _client;
    }
}