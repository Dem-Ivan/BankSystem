using System.Linq.Expressions;
using BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces;

public interface IContractRepository
{
    Contract Get(Expression<Func<Contract, bool>> exception);
    IEnumerable<ContractHistoryElement> GetContractHistory();
    void Add(Contract contract);
    void AddContractHistoryElement(ContractHistoryElement contractHistoryElement);   
}