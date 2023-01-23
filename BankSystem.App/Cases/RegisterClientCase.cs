using BankSystem.App.Interfaces;

namespace BankSystem.App.Cases
{
    public class RegisterClientCase
    {
        // ReSharper disable once NotAccessedField.Local
        private IClientRepository _clientRepository;
        public RegisterClientCase(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        //CRUD mthods
    }
}
