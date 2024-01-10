using System.Linq.Expressions;
using BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces;

public interface IContractRepository
{
    Task<Contract> GetAsync(Expression<Func<Contract, bool>> expression);   
    Task AddAsync(Contract contract);
    Task AddContractHistoryElementAsync(ContractHistoryElement contractHistoryElement);
    Task<IEnumerable<Contract>> GetUnSignedContracts();
    Task<int> GetLastContractNumberAsync();
}