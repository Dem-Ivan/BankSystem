using BankSystem.Domain.Exceptions;

namespace BankSystem.Domain.Models
{
    public class Employee
    {
        private string _name;
        private int _age;

        public string Name
        {
            set
            {
                if (value == null || value == "")
                {
                    throw new InvalidPersonDataException("Имя сотрудника обязательно.");
                }
                _name = value;
            }
            get { return _name; }
        }

        public int Age
        {
            set
            {
                if (value < 18)
                {
                    throw new InvalidPersonDataException("Минимальный возраст сотрудника равен 18 годам.");
                }
                _age = value;
            }
            get { return _age; }
        }

        public Role Role { get; set; }

    }
}
