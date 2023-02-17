using System.Linq.Expressions;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.API.Repositories;

public class ContractRepository : IContractRepository
{
    private BankSystemDbContext _bankSystemDbContext;

    public ContractRepository(BankSystemDbContext bankSystemDbContext)
    {
        _bankSystemDbContext = bankSystemDbContext;
    }

    public Contract Get(Expression<Func<Contract, bool>> exception)
    {
        return _bankSystemDbContext.Contract.Include(c => c.History).FirstOrDefault(exception);
    }

    public void Add(Contract contract)
    {
        _bankSystemDbContext.Contract.Add(contract);
    }

   
    public IEnumerable<ContractHistoryElement> GetContractHistory()
    {
        return _bankSystemDbContext.ContractHistory;
    }

    public void AddContractHistoryElement(ContractHistoryElement contractHistoryElement)
    {
        _bankSystemDbContext.ContractHistory.Add(contractHistoryElement);
    }    
}