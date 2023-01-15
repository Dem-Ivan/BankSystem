using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Exceptions
{
    public class InvalidAccessException : Exception
    {
        public InvalidAccessException(string message) : base(message)
        {

        }
    }
}
