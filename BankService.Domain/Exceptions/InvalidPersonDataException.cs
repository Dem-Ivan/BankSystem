using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Exceptions
{
    public class InvalidPersonDataException : Exception
    {
        public InvalidPersonDataException(string message) : base (message) 
        {

        }
    }
}
