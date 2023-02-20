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
    public async Task<Guid> CreateNewcontract(ContractTemplate template, Guid authorId, Guid counteragentId)
    {
        var author = await _unitOfWork.Employees.GetAsync(authorId);
        if (author == null)
        {
            throw new NotFoundException($"Сотруднк с идентификатором {authorId} не зарегистрирован в системе.");
        }

        var counteragent = await _unitOfWork.Clients.GetAsync(counteragentId);
        if (counteragent == null)
        {
            throw new NotFoundException($"Клиент с идентификатором {counteragentId} не зарегистрирован в системе.");
        }

        var contract =  template.GetNewContract(author, counteragent);
        contract.CreationDate = DateTime.UtcNow.Date;
        await _unitOfWork.Contracts.AddAsync(contract);
        await _unitOfWork.SaveAsync();

        return contract.Id;
    }

    public async Task<ContractResponse> CompleteContract(Guid counteragentId, Guid contractId)
    {
        var counteragent = await _unitOfWork.Clients.GetAsync(counteragentId);
        if (counteragent == null)
        {
            throw new NotFoundException($"Клиент с идентификатором {counteragentId} не зарегистрирован в системе.");
        }

        var contract = await _unitOfWork.Contracts.GetAsync(new ContractStatusSpecification(contractId, Status.Created));
        if (contract == null)
        {
            throw new NotFoundException($"Контракт с идентификатором {contractId} не зарегистрирован в системе или у него не подходящий статус.");
        }

        contract.Сomplete();
        await _unitOfWork.Contracts.AddContractHistoryElementAsync(contract.History.LastOrDefault());
        contract.SendforAcquaintance();
        await _unitOfWork.Contracts.AddContractHistoryElementAsync(contract.History.LastOrDefault());
        await _unitOfWork.SaveAsync();

        var result =  _mapper.Map<ContractResponse>(contract);
        return result;
    }

    public async Task<Guid> СonfirmAcquaintance(Guid counteragentId, Guid contractId)
    {
        var contract = await _unitOfWork.Contracts.GetAsync(new ContractStatusSpecification(contractId, Status.ForAcquaintance));
        if (contract == null)
        {
            throw new NotFoundException($"Контракт с идентификатором {contractId} не зарегистрирован в системе или у него не подходящий статус.");
        }

        var counteragent = await _unitOfWork.Clients.GetAsync(counteragentId);
        if (counteragent == null)
        {
            throw new NotFoundException($"Клиент с идентификатором {counteragentId} не зарегистрирован в системе.");
        }

        contract.Cquaint(counteragent);
        await _unitOfWork.Contracts.AddContractHistoryElementAsync(contract.History.LastOrDefault());
        await _unitOfWork.SaveAsync();

        return contract.Id;
    }

    public async Task SignContract(Guid signerId, Guid contractId)
    {
        var contract = await _unitOfWork.Contracts.GetAsync(new ContractStatusSpecification(contractId, Status.ForSigning));
        if (contract == null)
        {
            throw new NotFoundException($"Контракт с идентификатором {contractId} не зарегистрирован в системе или у него не подходящий статус.");
        }

        var signer = await _unitOfWork.Employees.GetAsync(signerId);
        if (signer == null)
        {
            throw new NotFoundException($"Сотрудник с идентификатором {signerId} не зарегистрирован в системе.");
        }

        contract.Sign(signer);
        await _unitOfWork.Contracts.AddContractHistoryElementAsync(contract.History.LastOrDefault());
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateContractBody(Guid contractId, Guid redactorId, string newBody)
    {
        var contract = await _unitOfWork.Contracts.GetAsync(new ContractStatusSpecification(contractId, Status.ForAcquaintance));
        if (contract == null)
        {
            throw new NotFoundException($"Контракт с идентификатором {contractId} не зарегистрирован в системе или у него не подходящий статус.");
        }

        var redactor = await _unitOfWork.Employees.GetAsync(redactorId);
        if (redactor == null)
        {
            throw new NotFoundException($"Сотрудник с идентификатором {redactorId} не зарегистрирован в системе.");
        }

        if (contract.AuthorId != redactor.Id)
        {
            throw new InvalidAccessException($"Сотрудник {redactor.Name} не является автором кнтракта! Редактировать контракт может только его автор.");
        }

        contract.UpdateBody(newBody);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteContract(Guid contractId)
    {
        var contract = await _unitOfWork.Contracts.GetAsync(new ContractStatusSpecification(contractId, Status.Signed));
        if (contract == null)
        {
            throw new NotFoundException($"Контракт с идентификатором {contractId} не зарегистрирован в системе или у него не подходящий статус.");
        }

        contract.DeletedDate = DateTime.UtcNow.Date;
        await _unitOfWork.SaveAsync();
    }
}