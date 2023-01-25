using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankSystem.API.Repositoryes
{
    public class EmployeeRepository : IEmployeeRepository
    {      
        private List<Employee> _employees= new List<Employee>(); // TODO: заменить на работу с базой
        
        public void Add(Employee employee)
        {
            _employees.Add(employee);
        }

        public void Delete(Guid employeeId)
        {
            
        }        

        public void Update(Employee employee)
        {
           
        }

        public Employee Get(Guid employeeId)
        {
            return _employees.FirstOrDefault(x => x.Id == employeeId);
        }
    }
}
