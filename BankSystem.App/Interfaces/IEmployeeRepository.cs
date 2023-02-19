using  BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces;

public interface IEmployeeRepository
{
    Task<Employee> GetAsync(Guid employeeId);
    Task AddAsync(Employee employee);       
}