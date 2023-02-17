using System;
using BankSystem.Domain.Models;
using Dex.Specifications;

namespace BankSystem.App.Specifications.Contract;

public class ContractStatusSpecification : AndSpecification<Domain.Models.Contract>
{
    public ContractStatusSpecification(Guid contractId, Status status)
        : base(new Specification<Domain.Models.Contract>(co => co.Status == status), new ExistContractSpecification(contractId))
    {

    }
}
