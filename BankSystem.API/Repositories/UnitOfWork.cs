using BankSystem.App.Interfaces;

namespace BankSystem.API.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IEmployeeRepository _employeeRepository;
    private IClientRepository _clientRepository;
    private IContractRepository _contractRepository;
    private BankSystemDbContext _bankSystemDbContext;

    public IEmployeeRepository Employees => _employeeRepository ??= new EmployeeRepository(_bankSystemDbContext);

    public IClientRepository Clients => _clientRepository ??= new ClientRepository(_bankSystemDbContext);

    public IContractRepository Contracts => _contractRepository ??= new ContractRepository(_bankSystemDbContext);

    public UnitOfWork(BankSystemDbContext bankSystemDbContext)
    {
        _bankSystemDbContext = bankSystemDbContext;
    }

    public void Save()
    {
        _bankSystemDbContext.SaveChanges();
    }

    private bool _disposed = false;

    public virtual void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                _bankSystemDbContext.Dispose();
            }

            this._disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}