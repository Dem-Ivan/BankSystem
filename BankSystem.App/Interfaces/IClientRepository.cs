using BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces;

public interface IClientRepository
{
    Client Get(Guid clientId);
    void Add(Client client);        
}