using System.Linq.Expressions;
using BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces;

public interface IContractRepository
{
    Task<Contract> GetAsync(Expression<Func<Contract, bool>> exception);   
    Task AddAsync(Contract contract);
    Task AddContractHistoryElementAsync(ContractHistoryElement contractHistoryElement);   
}