using System;
using Dex.Specifications;

namespace BankSystem.App.Specifications.Contract;

public class ExistContractSpecification : AndSpecification<Domain.Models.Contract>
{
    public ExistContractSpecification(Guid contractId)
        : base(new Specification<Domain.Models.Contract>(co => co.Id == contractId), new UndeleteContractSpecification())
    {

    }
}

