namespace BankSystem.App.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public IEmployeeRepository Employees { get; }
    public IClientRepository Clients { get; }
    public IContractRepository Contracts { get; }

    public void Save();
}