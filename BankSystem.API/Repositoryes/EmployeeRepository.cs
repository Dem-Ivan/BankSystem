using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using System;
using System.Linq;

namespace BankSystem.API.Repositoryes
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private BankSystemDbContext _bankSystemDbContext;

        public EmployeeRepository(BankSystemDbContext bankSystemDbContext)
        {
            _bankSystemDbContext = bankSystemDbContext;
        }


        public Employee Get(Guid employeeId)
        {
            return _bankSystemDbContext.Employee.FirstOrDefault(x => x.Id == employeeId);
        }

        public void Add(Employee employee)
        {
            _bankSystemDbContext.Employee.Add(employee);
        }

        public void Delete(Guid employeeId)
        {
            var employee = _bankSystemDbContext.Employee.FirstOrDefault(c => c.Id == employeeId);
            _bankSystemDbContext.Employee.Remove(employee);
        }

        public void Update(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _bankSystemDbContext.SaveChanges();
        }
    }
}
