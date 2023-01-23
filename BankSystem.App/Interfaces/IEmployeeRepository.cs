using  BankSystem.Domain.Models;
using System;
using System.Collections.Generic;

namespace BankSystem.App.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Get();
        Employee GetEmployee(Guid employeeId);
        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(Guid employeeId);
    }
}
