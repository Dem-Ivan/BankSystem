using  BankSystem.Domain.Models;

namespace BankSystem.App.Interfaces;

public interface IEmployeeRepository
{
    Employee Get(Guid employeeId);
    void Add(Employee employee);       
}