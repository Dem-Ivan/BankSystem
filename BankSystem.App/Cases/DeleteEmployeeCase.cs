using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;

namespace BankSystem.App.Cases;
internal class DeleteEmployeeCase
{
    private IUnitOfWork _unitOfWork;

    public DeleteEmployeeCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task DeleteEmploye(Guid employeeId)
    {
        var employee = await _unitOfWork.Employees.GetAsync(employeeId);
        if (employee == null)
        {
            throw new NotFoundException($"Сотрудник с идентификатором {employeeId} не зарегистрирован в системе.");
        }

        employee.DeletedDate = DateTime.UtcNow.Date;
        await _unitOfWork.SaveAsync();
    }
}
