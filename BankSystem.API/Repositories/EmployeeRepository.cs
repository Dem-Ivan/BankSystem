using System.Diagnostics.Contracts;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.API.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private BankSystemDbContext _bankSystemDbContext;

    public EmployeeRepository(BankSystemDbContext bankSystemDbContext)
    {
        _bankSystemDbContext = bankSystemDbContext;
    }


    public async Task<Employee> GetAsync(Guid employeeId)
    {
        return await _bankSystemDbContext.Employee.FirstOrDefaultAsync(x => x.Id == employeeId).ConfigureAwait(false);
    }

    public async Task AddAsync(Employee employee)
    {
        await _bankSystemDbContext.Employee.AddAsync(employee).ConfigureAwait(false);
    }      
}