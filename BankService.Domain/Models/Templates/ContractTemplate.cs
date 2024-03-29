﻿
namespace BankSystem.Domain.Models.Templates;

public class ContractTemplate
{
    private static ContractTemplate _template;
    private Role _signerRole = Role.Director;

    public Role SignerRole
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

    public Contract GetNewContract(Employee author, Client counteragent)
    {
        return new Contract(this, author, counteragent);
    }
}