
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
        private readonly List<ContractHistoryElement> _historyItems;
        private readonly Employee _author;
        private  Client _counteragent;

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
            get => _template.SignerRole;
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


        public IReadOnlyCollection<ContractHistoryElement> History => _historyItems;

        public Contract()
        {

        }

        public Contract(ContractTemplate template, Employee author)
        {
            _template = template;            
            _historyItems = new List<ContractHistoryElement>();
            AuthorId = author.Id;
            _author = author;
            Status = Status.created;

            UpdateHistori();
        }

        public void Сomplete(Client counteragent)
        {
            CounteragentId = counteragent.Id;
            _counteragent = counteragent;
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
