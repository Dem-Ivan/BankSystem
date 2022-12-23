
using BankSystem.Domain.Exceptions;
using System;

namespace BankSystem.Domain.Models
{
    public class Contract
    {

        private Status _status = Status.created;
        private string _body;

        public Status Status
        {
            get { return _status; }            
        }

        public  string Body
        {
            get {return _body;}
        }


        public Contract()
        {
            _status = Status.created;
        }

        public void Сomplete(Employee counteragent)
        {
            _body = $"Контракт с {counteragent.Name} заключен {DateTime.Now}.";
            _status = Status.completed;
        }

        public void Sign(Employee signer)
        {          
            if (signer.Role == Role.director)
            {
                _body = _body + $"Подписан - {signer.Name} {DateTime.Now.ToString()}";
                _status = Status.created;
            }
            else
            {
                throw new InvalidRoleException("Подписать договор может только руководитель");
            }
        }

    }
}
