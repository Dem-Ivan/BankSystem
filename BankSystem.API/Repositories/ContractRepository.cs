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

    public async Task<Contract> GetAsync(Expression<Func<Contract, bool>> expression)
    {
        return await _bankSystemDbContext.Contract.Include(c => c.History)
            .FirstOrDefaultAsync(expression).ConfigureAwait(false);
    }

    public async Task<IEnumerable<Contract>> GetUnSignedContracts()
    {
        return await _bankSystemDbContext.Contract.Where(c => c.Status == Status.ForSigning && c.DeletedDate == null).ToListAsync();
    }

    public async Task<int> GetLastContractNumberAsync()
    {
        return  await _bankSystemDbContext.Contract.MaxAsync(c => c.Number);
    }

    public async Task AddAsync(Contract contract)
    {
        await _bankSystemDbContext.Contract.AddAsync(contract).ConfigureAwait(false);
    }


    public async Task AddContractHistoryElementAsync(ContractHistoryElement contractHistoryElement)
    {
        await _bankSystemDbContext.ContractHistory.AddAsync(contractHistoryElement).ConfigureAwait(false);
    }    
}