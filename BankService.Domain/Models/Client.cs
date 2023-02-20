using BankSystem.Domain.Exceptions;

namespace BankSystem.Domain.Models;

public class Client
{
    private readonly string _name;
    private readonly int _age;

    public Guid Id { get; } = Guid.NewGuid();

    public string Name
    {
        init => _name = value;
       
        get => _name;
    }

    public int Age
    {
        init => _age = value;
        
        get => _age;
    }

    public string Email { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Contract Contract { get; set; }
    //TOTO: тут какойто метод характерный для клиента
}