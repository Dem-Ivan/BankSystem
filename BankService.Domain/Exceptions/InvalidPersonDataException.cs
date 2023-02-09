namespace BankSystem.Domain.Exceptions;

public class InvalidPersonDataException : Exception
{
    public InvalidPersonDataException(string message) : base (message)
    {

    }
}