using BankSystem.Domain.Exceptions;

namespace BankSystem.Domain.Models
{
    public class Client
    {
        private string _name;
        private int _age;
        public string Name 
        {
            set
            {
                if (value == null || value == "")
                {
                    throw new InvalidPersonDataException("Имя клиента обязательно.");
                }
            }
            get { return _name; } 
        }


        public int Age
        {
            set
            {
                if (value <18)
                {
                    throw new InvalidPersonDataException("Минимальный возраст клиента равен 18 годам.");
                }
            }
            get { return _age; }
        }

        //TOTO: тут какойто метод характерный для клиента
    }
}
