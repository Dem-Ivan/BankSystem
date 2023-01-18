using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Models.Templates
{
    public class ContractTemplate
    {
        private static ContractTemplate _template;
        protected role _signerRole;

        public role SignerRole
        {
            set
            { //TODO: Предположим тут проверка что это действие совершает админ
                _signerRole = value;
            }

            get => _signerRole;
        }

        public static ContractTemplate GetInstance()
        {
            if (_template == null)
                _template = new ContractTemplate();

            return _template;
        }

        public Contract GetNewContract()
        {
            return new Contract(this);
        }
    }
}
