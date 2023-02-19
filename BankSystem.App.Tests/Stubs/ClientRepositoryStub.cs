using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.App.Tests.Stubs;

public class ClientRepositoryStub : IClientRepository
{
    private Client _client;

    public async Task AddAsync(Client client)
    {
        _client = client;
    }

    public async Task<Client> GetAsync(Guid clientId)
    {
        return _client;
    }   
}