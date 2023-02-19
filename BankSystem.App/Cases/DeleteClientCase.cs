using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;

namespace BankSystem.App.Cases;
internal class DeleteClientCase
{
    private IUnitOfWork _unitOfWork;

    public DeleteClientCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;   
    }

    public async Task DeleteClent(Guid clientId)
    {
        var client = await _unitOfWork.Clients.GetAsync(clientId);
        if (client == null)
        {
            throw new NotFoundException($"Клиент с идентификатором {clientId} не зарегистрирован в системе.");
        }

        client.DeletedDate = DateTime.UtcNow.Date;
        await _unitOfWork.SaveAsync();
    }
}
