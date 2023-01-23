using System;
using System.Collections.Generic;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.App.Cases
{
    public class RegisterEmployeeCase
    {
        // ReSharper disable once NotAccessedField.Local
        private IEmployeeRepository _employeeRepository;
        public RegisterEmployeeCase(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            //обращаемся к _employeeRepository,
            //получаем и возвращяем коллекцию сотрудников
            return null;
        }
        public void AddEmploye(Employee employee)
        {

        }

        public void DeleteEmploye(Guid employeeId)
        {

        }

        public void UpdateEmploye(Employee employee)
        {

        }
    }
}
