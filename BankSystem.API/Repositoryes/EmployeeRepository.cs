using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using System;
using System.Collections.Generic;

namespace BankSystem.API.Repositoryes
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public EmployeeRepository()
        {
        }

        public IEnumerable<Employee> Get()
        {
            //обращаемся к _dbContext,
            //получаем и возвращяем коллекцию сотрудников
            return null;
        }
        public void Add(Employee employee)
        {
            
        }

        public void Delete(Guid employeeId)
        {
            
        }        

        public void Update(Employee employee)
        {
           
        }
    }
}
