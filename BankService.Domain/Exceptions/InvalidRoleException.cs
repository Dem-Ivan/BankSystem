using System;

namespace BankSystem.Domain.Exceptions
{
    public class InvalidRoleException : Exception
    {
        public InvalidRoleException(string message) : base(message)
        {

        }
    }
}
