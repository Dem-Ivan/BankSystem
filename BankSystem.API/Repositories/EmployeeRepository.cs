using System.Diagnostics.Contracts;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.API.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private BankSystemDbContext _bankSystemDbContext;

    public EmployeeRepository(BankSystemDbContext bankSystemDbContext)
    {
        _bankSystemDbContext = bankSystemDbContext;
    }


    public Employee Get(Guid employeeId)
    {
        return _bankSystemDbContext.Employee.FirstOrDefault(x => x.Id == employeeId);
    }

    public void Add(Employee employee)
    {
        _bankSystemDbContext.Employee.Add(employee);
    }      
}