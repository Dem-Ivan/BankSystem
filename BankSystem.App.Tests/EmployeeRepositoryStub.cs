using System;
using System.Collections.Generic;
using System.Linq;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.App.Tests
{
    public class EmployeeRepositoryStub : IEmployeeRepository
    {
        private List<Employee> _employees;

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
            return _employees.FirstOrDefault(x => x.EmployeeId == employeeId);
        }

        public void Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
