
using BankSystem.Domain.Exceptions;
using BankSystem.Domain.Models.Templates;
using System;
using System.Collections.Generic;

namespace BankSystem.Domain.Models
{
    public class Contract 
    {
        private Guid _contractId = Guid.NewGuid();
        private Status _status = Status.created;
        private string _body;
        private ContractTemplate _template;
        private readonly List<ContractHistory> _historyItems;

        public Guid ContractId
        {
            get => _contractId;
        }

        public Status Status
        {
            private init
            {
                _status = value;
            }

            get => _status;
        }

        public string Body
        {
            get => _body;
        }

        public role SignerRole
        {
            get => _template.SignerRole;
        }

        public IReadOnlyCollection<ContractHistory> History => _historyItems;

        public Contract(ContractTemplate template)
        {
            _template = template;
            Status = Status.created;
            _historyItems = new List<ContractHistory>();

            UpdateHistori();
        }

        public void Сomplete(Client counteragent)
        {
            _body = $"Контракт с {counteragent.Name} заключен {DateTime.Now}.";
            _status = Status.completed;

            UpdateHistori();
        }

        public void SendforAcquaintance(Client client)
        {
            _status = Status.forAcquaintance;

            UpdateHistori();
            //TODO: направить пользователью на ознакомление
        }

        public void Cquaint(Client client)
        {
            if (!_body.Contains(client.Name))
            {
                throw new InvalidAccessException("Подтвердить факт ознакомления с контрактом " +
                    "может только пользователь с которым контракт заключается!");
            }
            _status = Status.forSigning;

            UpdateHistori();
        }

        public void Sign(Employee signer)
        {
            if (signer.Role != SignerRole)
            {
                throw new InvalidRoleException($"Подписать договор может только сотрудник с ролью {SignerRole}");
            }

            _body = _body + $"Подписан - {signer.Name} {DateTime.Now}";
            _status = Status.signed;

            UpdateHistori();           
        }

        private void UpdateHistori()
        {
            _historyItems.Add(new ContractHistory(this));
        }
    }
}
