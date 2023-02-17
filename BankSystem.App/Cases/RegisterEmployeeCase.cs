using AutoMapper;
using BankSystem.App.DTO;
using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.App.Cases;

public class RegisterEmployeeCase
{
    private IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterEmployeeCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public EmployeeResponse Get(Guid employeeId)
    {
        var employee = _unitOfWork.Employees.Get(employeeId);
        if (employee == null)
        {
            throw new NotFoundException($"Сотрудник с идентификатором {employeeId} не зарегистрирован в системе.");
        }

        var mappedEmployee = _mapper.Map(employee, new EmployeeResponse());

        return mappedEmployee;
    }

    public Guid AddEmployee(EmployeeRequest employee)
    {
        var mappedEmployee = _mapper.Map<Employee>(employee);
        mappedEmployee.CreationDate = DateTime.UtcNow.Date;

        _unitOfWork.Employees.Add(mappedEmployee);
        _unitOfWork.Save();

        return mappedEmployee.Id;
    }

    public void DeleteEmploye(Guid employeeId)
    {
        var employee = _unitOfWork.Employees.Get(employeeId);
        if (employee == null)
        {
            throw new NotFoundException($"Сотрудник с идентификатором {employeeId} не зарегистрирован в системе.");
        }

        employee.DeletedDate = DateTime.UtcNow.Date;
        _unitOfWork.Save();
    }

    public void UpdateEmploye(EmployeeRequest employee)
    {

    }
}