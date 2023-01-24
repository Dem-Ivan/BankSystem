using System;
using BankSystem.Domain.Exceptions;

namespace BankSystem.Domain.Models
{
    public class Client
    {
        private Guid _id = Guid.NewGuid();
        private readonly string _name;
        private int _age;

        public Client(int age, string name)
        {
            Age = age;
            Name = name;
        }

        public Guid Id
        {
            get => _id;
        }

        public string Name
        {
            private init
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidPersonDataException("Имя клиента обязательно.");
                }

                _name = value;
            }
            get => _name;
        }

        public int Age
        {
            private init
            {
                if (value < 18)
                {
                    throw new InvalidPersonDataException("Минимальный возраст клиента равен 18 годам.");
                }

                _age = value;
            }
            get => _age;
        }

        //TOTO: тут какойто метод характерный для клиента
    }
}
