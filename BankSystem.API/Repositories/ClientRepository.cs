using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.API.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly BankSystemDbContext _bankSystemDbContext;

    public ClientRepository(BankSystemDbContext bankSystemDbContext)
    {
        _bankSystemDbContext = bankSystemDbContext;
    }

    public async Task<Client> GetAsync(Guid clientId)
    {
        return await _bankSystemDbContext.Client.FirstOrDefaultAsync(x => x.Id == clientId).ConfigureAwait(false);
    }

    public async Task AddAsync(Client client)
    {
       await _bankSystemDbContext.Client.AddAsync(client).ConfigureAwait(false);
    }    
}