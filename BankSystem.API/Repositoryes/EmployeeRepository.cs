using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using System;
using System.Collections.Generic;

namespace BankSystem.API.Repositoryes
{
    public class EmployeeRepository : IEmployeeRepository
    {      
        
        public void Add(Employee employee)
        {
            
        }

        public void Delete(Guid employeeId)
        {
            
        }        

        public void Update(Employee employee)
        {
           
        }

        public Employee Get(Guid employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
