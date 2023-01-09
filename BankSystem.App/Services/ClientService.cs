using BankSystem.App.Interfaces;

namespace BankSystem.App.Services
{
    public class ClientService
    {
        // ReSharper disable once NotAccessedField.Local
        private IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
    }
}
