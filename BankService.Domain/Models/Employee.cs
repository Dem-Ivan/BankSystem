using System;
using BankSystem.Domain.Exceptions;

namespace BankSystem.Domain.Models
{
    public class Employee
    {
        private Guid _employeeId = Guid.NewGuid();
        private readonly string _name;
        private int _age;

        public Employee(int age, string name, role role)
        {
            Age = age;
            Name = name;
            Role = role;
        }

        public Guid EmployeeId
        {
            get => _employeeId;
        }

        public string Name
        {
            private init
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidPersonDataException("Имя сотрудника обязательно.");
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
                    throw new InvalidPersonDataException("Минимальный возраст сотрудника равен 18 годам.");
                }
                _age = value;
            }
            get => _age; 
        }

        public role Role { get; set; }
    }
}
