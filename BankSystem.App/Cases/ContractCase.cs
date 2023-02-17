using BankSystem.App.DTO;
using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Exceptions;
using BankSystem.Domain.Models.Templates;
using AutoMapper;
using BankSystem.App.Specifications.Contract;
using BankSystem.Domain.Models;

namespace BankSystem.App.Cases;

public class ContractCase
{
    private IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ContractCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    //TODO: метод вызывается в контроллере с аутентификацией сотрудника
    //TODO:  надобы добавить таблицу шаблонов, и трать шаблон из нее
    public Guid CreateNewcontract(ContractTemplate template, Guid authorId, Guid counteragentId)
    {
        var author = _unitOfWork.Employees.Get(authorId);
        if (author == null)
        {
            throw new NotFoundException($"Сотруднк с идентификатором {authorId} не зарегистрирован в системе.");
        }

        var counteragent = _unitOfWork.Clients.Get(counteragentId);
        if (counteragent == null)
        {
            throw new NotFoundException($"Клиент с идентификатором {counteragentId} не зарегистрирован в системе.");
        }

        var contract =  template.GetNewContract(author, counteragent);
        contract.CreationDate = DateTime.UtcNow.Date;
        _unitOfWork.Contracts.Add(contract);
        _unitOfWork.Save();

        return contract.Id;
    }

    public ContractResponse CompleteContract(Guid counteragentId, Guid contractId)
    {
        var counteragent = _unitOfWork.Clients.Get(counteragentId);
        if (counteragent == null)
        {
            throw new NotFoundException($"Клиент с идентификатором {counteragentId} не зарегистрирован в системе.");
        }

        var contract = _unitOfWork.Contracts.Get(new ContractStatusSpecification(contractId, Status.Created));
        if (contract == null)
        {
            throw new NotFoundException($"Контракт с идентификатором {contractId} не зарегистрирован в системе или у него не подходящий статус.");
        }

        contract.Сomplete(counteragent);
        _unitOfWork.Contracts.AddContractHistoryElement(contract.History.LastOrDefault());
        contract.SendforAcquaintance();
        _unitOfWork.Contracts.AddContractHistoryElement(contract.History.LastOrDefault());
        _unitOfWork.Save();

        var result =  _mapper.Map<ContractResponse>(contract);
        return result;
    }

    public Guid СonfirmAcquaintance(Guid counteragentId, Guid contractId)
    {
        var contract = _unitOfWork.Contracts.Get(new ContractStatusSpecification(contractId, Status.ForAcquaintance));
        if (contract == null)
        {
            throw new NotFoundException($"Контракт с идентификатором {contractId} не зарегистрирован в системе или у него не подходящий статус.");
        }

        var counteragent = _unitOfWork.Clients.Get(counteragentId);
        if (counteragent == null)
        {
            throw new NotFoundException($"Клиент с идентификатором {counteragentId} не зарегистрирован в системе.");
        }

        contract.Cquaint(counteragent);
        _unitOfWork.Contracts.AddContractHistoryElement(contract.History.LastOrDefault());
        _unitOfWork.Save();

        return contract.Id;
    }

    public void SignContract(Guid signerId, Guid contractId)
    {
        var contract = _unitOfWork.Contracts.Get(new ContractStatusSpecification(contractId, Status.ForSigning));
        if (contract == null)
        {
            throw new NotFoundException($"Контракт с идентификатором {contractId} не зарегистрирован в системе или у него не подходящий статус.");
        }

        var signer = _unitOfWork.Employees.Get(signerId);
        if (signer == null)
        {
            throw new NotFoundException($"Сотрудник с идентификатором {signerId} не зарегистрирован в системе.");
        }

        contract.Sign(signer);
        _unitOfWork.Contracts.AddContractHistoryElement(contract.History.LastOrDefault());
        _unitOfWork.Save();
    }

    public void UpdateContractBody(Guid contractId, Guid redactorId, string newBody)
    {
        var contract = _unitOfWork.Contracts.Get(new ContractStatusSpecification(contractId, Status.ForAcquaintance));
        if (contract == null)
        {
            throw new NotFoundException($"Контракт с идентификатором {contractId} не зарегистрирован в системе или у него не подходящий статус.");
        }

        var redactor = _unitOfWork.Employees.Get(redactorId);
        if (redactor == null)
        {
            throw new NotFoundException($"Сотрудник с идентификатором {redactorId} не зарегистрирован в системе.");
        }

        if (contract.AuthorId != redactor.Id)
        {
            throw new InvalidAccessException($"Сотрудник {redactor.Name} не является автором кнтракта! Редактировать контракт может только его автор.");
        }

        contract.UpdateBody(newBody);
        _unitOfWork.Save();
    }

    public void DeleteContract(Guid contractId)
    {
        var contract = _unitOfWork.Contracts.Get(new ContractStatusSpecification(contractId, Status.Signed));
        if (contract == null)
        {
            throw new NotFoundException($"Контракт с идентификатором {contractId} не зарегистрирован в системе или у него не подходящий статус.");
        }

        contract.DeletedDate = DateTime.UtcNow.Date;
        _unitOfWork.Save();
    }
}