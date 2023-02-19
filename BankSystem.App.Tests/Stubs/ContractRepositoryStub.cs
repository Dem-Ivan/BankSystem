using System.Linq.Expressions;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.App.Tests.Stubs;

public class ContractRepositoryStub : IContractRepository
{
    private Contract _contract;
    public async Task AddAsync(Contract contract)
    {
        _contract = contract;
    }

    public async Task AddContractHistoryElementAsync(ContractHistoryElement contractHistoryElement)
    {
           
    } 

    public async Task<Contract> GetAsync(Expression<Func<Contract, bool>> exception)
    {
        return _contract;
    }    
}