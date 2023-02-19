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

    public async Task<EmployeeResponse> Get(Guid employeeId)
    {
        var employee = await _unitOfWork.Employees.GetAsync(employeeId);
        if (employee == null)
        {
            throw new NotFoundException($"Сотрудник с идентификатором {employeeId} не зарегистрирован в системе.");
        }

        var mappedEmployee = _mapper.Map(employee, new EmployeeResponse());

        return mappedEmployee;
    }

    public async Task<Guid> AddEmployee(EmployeeRequest employee)
    {
        var mappedEmployee = _mapper.Map<Employee>(employee);
        mappedEmployee.CreationDate = DateTime.UtcNow.Date;

        await _unitOfWork.Employees.AddAsync(mappedEmployee);
        await _unitOfWork.SaveAsync();

        return mappedEmployee.Id;
    }   
}