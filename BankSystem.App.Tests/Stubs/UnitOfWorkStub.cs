using BankSystem.App.Interfaces;

namespace BankSystem.App.Tests.Stubs;
internal class UnitOfWorkStub : IUnitOfWork
{
    private IEmployeeRepository _employeeRepository;
    private IClientRepository _clientRepository;
    private IContractRepository _contractRepository;

    public IEmployeeRepository Employees => _employeeRepository ??= new EmployeeRepositoryStub();

    public IClientRepository Clients => _clientRepository ??= new ClientRepositoryStub();

    public IContractRepository Contracts => _contractRepository ??= new ContractRepositoryStub();


    public UnitOfWorkStub(IEmployeeRepository employeeRepository, IClientRepository clientRepository, IContractRepository contractRepository)
    {
        _employeeRepository = employeeRepository;
        _clientRepository = clientRepository;
        _contractRepository = contractRepository;
    }
    
    public void Dispose()
    {
       
    }

    public async Task SaveAsync()
    {
        
    }
}
