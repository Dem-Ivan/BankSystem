using BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces;

public interface IClientRepository
{
    Task<Client> GetAsync(Guid clientId);
    Task AddAsync(Client client);        
}