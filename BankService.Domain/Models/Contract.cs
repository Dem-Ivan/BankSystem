﻿using BankSystem.Domain.Exceptions;
using BankSystem.Domain.Models.Templates;

namespace BankSystem.Domain.Models;

public class Contract
{
    private Status _status = Status.Created;
    private string _body;
    private ContractTemplate _template;
    private List<ContractHistoryElement> _historyItems;
    private Client _counteragent;

    public Guid Id { get; } = Guid.NewGuid();

    public Status Status
    {
        init => _status = value;

        get => _status;
    }

    public string Body => _body;

    public Role SignerRole { init; get; }

    public Guid AuthorId { get; set; }

    public Employee Author { init; get; }

    public Guid CounteragentId { get; set; }

    public Client Counteragent => _counteragent;

    public ICollection<ContractHistoryElement> History//public IReadOnlyCollection<ContractHistoryElement> History
    {
        get => _historyItems.AsReadOnly();
        init => _historyItems = value.ToList(); // наверно плохо, надо играться
    }

    public Contract()
    {
    }

    public Contract(ContractTemplate template, Employee author, Client counteragent)
    {
        _template = template;
        _historyItems = new List<ContractHistoryElement>();
        AuthorId = author.Id;
        Author = author;
        CounteragentId = counteragent.Id;
        _counteragent = counteragent;
        Status = Status.Created;
        SignerRole = _template.SignerRole;

        UpdateHistory();
    }

    public void Сomplete(Client counteragent)
    {
        _body = $"Контракт с {_counteragent.Name} заключен {DateTime.Now}.";
        _status = Status.Completed;

        UpdateHistory();
    }

    public void SendforAcquaintance()
    {
        _status = Status.ForAcquaintance;
        UpdateHistory();
    }

    public void Cquaint(Client client)
    {
        if (!_body.Contains(client.Name))
        {
            throw new InvalidAccessException("Подтвердить факт ознакомления с контрактом " +
                                             "может только пользователь с которым контракт заключается!");
        }

        _status = Status.ForSigning;

        UpdateHistory();
    }

    public void Sign(Employee signer)
    {
        if (signer.Role != SignerRole)
        {
            throw new InvalidRoleException($"Подписать договор может только сотрудник с ролью {SignerRole}");
        }

        _body += $"Подписан - {signer.Name} {DateTime.Now}";
        _status = Status.Signed;

        UpdateHistory();
    }

    public void UpdateBody(string newBody)
    {
        //TODO: подумать, напрашивается валидация на соответствие автора и радактора
        _body = newBody;
    }

    private void UpdateHistory()
    {
        _historyItems.Add(new ContractHistoryElement(this));
    }
}