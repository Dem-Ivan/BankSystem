using System.Linq.Expressions;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.App.Tests.Stubs;

public class ContractRepositoryStub : IContractRepository
{
    private Contract _contract;
    public void Add(Contract contract)
    {
        _contract = contract;
    }

    public void AddContractHistoryElement(ContractHistoryElement contractHistoryElement)
    {
           
    } 

    public Contract Get(Expression<Func<Contract, bool>> exception)
    {
        return _contract;
    }    

    public IEnumerable<ContractHistoryElement> GetContractHistory()
    {
        throw new NotImplementedException();
    }    
}