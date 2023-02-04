using System;
using System.Collections.Generic;
using AutoMapper;
using BankSystem.App.DTO;
using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.App.Cases
{
    public class RegisterEmployeeCase
    {        
        private IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public RegisterEmployeeCase(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public EmployeeResponse Get(Guid employeeId)
        {
            var employee = _employeeRepository.Get(employeeId);
            if (employee == null)
            {
                throw new NotFoundException($"Контракт с идентификатором {employeeId} не зарегистрирован в системе.");
            }

            var mappedEmployee = _mapper.Map(employee, new EmployeeResponse());

            return mappedEmployee;
        }

        public Guid AddEmployee(EmployeeRequest employee)
        {
            var mappedEmployee = _mapper.Map<Employee>(employee);

            _employeeRepository.Add(mappedEmployee);
            _employeeRepository.Save();

            return mappedEmployee.Id;
        }

        public void DeleteEmploye(Guid employeeId)
        {
            
        }

        public void UpdateEmploye(EmployeeRequest employee)
        {

        }
    }
}
