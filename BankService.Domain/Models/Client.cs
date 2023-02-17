using BankSystem.Domain.Exceptions;

namespace BankSystem.Domain.Models;

public class Client
{
    private readonly string _name;
    private readonly int _age;

    public Guid Id { get; } = Guid.NewGuid();

    public string Name
    {
        init
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
        init
        {
            if (value < 18)
            {
                throw new InvalidPersonDataException("Минимальный возраст клиента равен 18 годам.");
            }

            _age = value;
        }
        get => _age;
    }

    public string Email { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Contract Contract { get; set; }
    //TOTO: тут какойто метод характерный для клиента
}