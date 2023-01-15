
using BankSystem.Domain.Exceptions;
using System;

namespace BankSystem.Domain.Models
{
    public class Contract
    {

        private Status _status = Status.created;
        private string _body;
        private role _signerRole;

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

        public Contract(role signerRole)
        {
            SignerRole = signerRole;
            Status = Status.created;
        }

        public void Сomplete(Employee counteragent)
        {
            _body = $"Контракт с {counteragent.Name} заключен {DateTime.Now}.";
            _status = Status.completed;
            //TODO: Добавить запись о смене статуса в исорию
        }

        public void SendforAcquaintance(Client client)
        {
            _status = Status.forAcquaintance;
            //TODO: Добавить запись о смене статуса в исорию
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
            //TODO: Добавить запись о смене статуса в исорию
        }

        public void Sign(Employee signer)
        {
            if (signer.Role != SignerRole)
            {
                throw new InvalidRoleException($"Подписать договор может только сотрудник с ролью {SignerRole}");
            }

            _body = _body + $"Подписан - {signer.Name} {DateTime.Now}";
            _status = Status.signed;
            //TODO: Добавить запись о смене статуса в исорию
        }
    }
}
