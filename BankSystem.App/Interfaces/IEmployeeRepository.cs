using  BankSystem.Domain.Models;
using System;
using System.Collections.Generic;

namespace BankSystem.App.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Get();
        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(Guid employeeId);
    }
}
