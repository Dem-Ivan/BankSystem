using BankSystem.Domain.Exceptions;

namespace BankSystem.Domain.Models;

public class Employee
{
    private readonly string _name;
    private readonly int _age;

    public Employee(int age, string name, Role role)
    {
        Age = age;
        Name = name;
        Role = role;
    }

    public Guid Id { get; } = Guid.NewGuid();

    public string Name
    {
        private init
        {     
            _name = value;
        }
        get => _name;
    }

    public int Age
    {
        private init
        {   
            _age = value;
        }
        get => _age;
    }

    public string Email { get; set; }

    public Role Role { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public ICollection<Contract> Contracts { get; set; }
}