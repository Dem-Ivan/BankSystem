using System;
using BankSystem.API.Repositoryes;
using BankSystem.App.Interfaces;

namespace BankSystem.API
{
    public class UnitOfWork : IUnitOfWork
    {
        private IEmployeeRepository _employeeRepository;
        private IClientRepository _clientRepository;
        private IContractRepository _contractRepository;
        private BankSystemDbContext _bankSystemDbContext;

        public IEmployeeRepository Employees
        {
            get
            {
                if (_employeeRepository == null)
                {
                    _employeeRepository = new EmployeeRepository(_bankSystemDbContext);
                }
                return _employeeRepository;
            }
        }

        public IClientRepository Clients
        {
            get 
            {
                if (_clientRepository == null)
                {
                    _clientRepository = new ClientRepository(_bankSystemDbContext);
                }
                return _clientRepository;
            }
        }

        public IContractRepository Contracts
        {
            get
            {
                if (_contractRepository == null)
                {
                    _contractRepository = new ContractRepository(_bankSystemDbContext);
                }
                return _contractRepository;
            }
        }

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
}
