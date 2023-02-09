using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.App.Tests.Stubs;

public class EmployeeRepositoryStub : IEmployeeRepository
{
    private List<Employee> _employees = new();

    public void Add(Employee employee)
    {
        _employees.Add(employee);
    }

    public void Delete(Guid employeeId)
    {
        throw new NotImplementedException();
    }

    public Employee Get(Guid employeeId)
    {
        return _employees.FirstOrDefault(x => x.Id == employeeId);
    }

    public void Save()
    {
            
    }

    public void Update(Employee employee)
    {
        throw new NotImplementedException();
    }
}