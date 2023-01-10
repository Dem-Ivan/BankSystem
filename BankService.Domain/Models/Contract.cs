
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

            get =>_status;             
        }

        public  string Body
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

        public Contract( role signerRole)
        {
            SignerRole = signerRole;
            Status = Status.created;            
        }

        public void Сomplete(Employee counteragent)
        {
            _body = $"Контракт с {counteragent.Name} заключен {DateTime.Now}.";
            _status = Status.completed;
        }

        public void Sign(Employee signer)
        {          
            if (signer.Role == SignerRole)
            {
                _body = _body + $"Подписан - {signer.Name} {DateTime.Now}";
                _status = Status.created;
            }
            else
            {
                throw new InvalidRoleException($"Подписать договор может только сотрудник с ролью {SignerRole}");
            }
        }
    }
}
