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
        private readonly DateTime _changeDate = DateTime.Now;
        private readonly Status _newStatus;

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
            get => _changeDate;
        }

        public Status NewStatus
        {
            private init
            {
                _newStatus = value;
            }
            get => _newStatus;
        }

        public ContractHistory(Contract currentContract)
        {
            ContractID = currentContract.ContractId;
            NewStatus = currentContract.Status;
        }
    }
}
