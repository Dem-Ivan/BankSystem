using BankSystem.Domain.Exceptions;
using BankSystem.Domain.Models.Templates;
using System;
using System.Collections.Generic;

namespace BankSystem.Domain.Models
{
    public class Contract
    {
        private Guid _id = Guid.NewGuid();
        private Status _status = Status.created;
        private string _body;
        private ContractTemplate _template;
        private List<ContractHistoryElement> _historyItems;
        private Employee _author;
        private Client _counteragent;
        private role _signerRole;

        public Guid Id
        {
            get => _id;
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
            private init
            {
                _signerRole = value;
            }

            get => _signerRole;
        }

        public Guid AuthorId { get; set; }

        public Employee Author
        {
            private init
            {
                _author = value;
            }

            get => _author;
        }

        public Guid CounteragentId { get; set; }


        public Client Counteragent
        {
            get => _counteragent;
        }


        public List<ContractHistoryElement> History//public IReadOnlyCollection<ContractHistoryElement> History   
        {
            get => _historyItems;
            
            init
            {
                _historyItems =  value;
            }
        }

        public Contract()
        {

        }

        public Contract(ContractTemplate template, Employee author, Client counteragent)
        {
            _template = template;
            _historyItems = new List<ContractHistoryElement>();
            AuthorId = author.Id;
            _author = author;
            CounteragentId = counteragent.Id;
            _counteragent = counteragent;
            Status = Status.created;
            _signerRole = _template.SignerRole;

            UpdateHistori();
        }

        public void Сomplete(Client counteragent)
        {
            _body = $"Контракт с {_counteragent.Name} заключен {DateTime.Now}.";
            _status = Status.completed;

            UpdateHistori();
        }

        public void SendforAcquaintance()
        {
            _status = Status.forAcquaintance;
            UpdateHistori();
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


        public void UpdateBody(string newBody)
        {
            //TODO: подумать, напрашивается валидация на соответствие автора и радактора
            _body = newBody;
        }

        private void UpdateHistori()
        {
            _historyItems.Add(new ContractHistoryElement(this));
        }
    }
}
