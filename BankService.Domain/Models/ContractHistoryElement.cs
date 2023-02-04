using System;

namespace BankSystem.Domain.Models
{
    public class ContractHistoryElement
    {
        private Guid _id = Guid.NewGuid();
        private readonly DateTime _changeDate = DateTime.Now;
        private readonly Status _newStatus;

        public Guid Id
        {
            get => _id;
        }

        public Guid ContractId { get; }
        public Contract Contract { get; set; }

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

        public ContractHistoryElement()
        {

        }

        public ContractHistoryElement(Contract currentContract)
        {
            Contract = currentContract;
            NewStatus = currentContract.Status;
        }
    }
}
