using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.App.Tests.Stubs;

public class EmployeeRepositoryStub : IEmployeeRepository
{
    private List<Employee> _employees = new();

    public async Task AddAsync(Employee employee)
    {
        _employees.Add(employee);
    }
    public async Task<Employee> GetAsync(Guid employeeId)
    {
        return _employees.FirstOrDefault(x => x.Id == employeeId);
    }   
}