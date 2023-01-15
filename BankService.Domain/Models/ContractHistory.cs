using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Models
{
    public class ContractHistory
    {

        private readonly Guid _contractID;
        private readonly DateTime _changeDate;
        private readonly string _statusChange;

        public Guid ContractID
        {
            private init
            {
                _contractID = value;
            }
            get => _contractID;
        }

        public DateTime ChangeDate
        {            
            private init
            {
                _changeDate = DateTime.Now; 
            }

            get => _changeDate;
        }
        public string StatusChange
        {
            private init
            {
                _statusChange = value;
            }
            get => _statusChange;
        }

        public ContractHistory(Guid contractID, string statusChange)
        {
            StatusChange = statusChange;
            ContractID = contractID;
        }
    }
}
