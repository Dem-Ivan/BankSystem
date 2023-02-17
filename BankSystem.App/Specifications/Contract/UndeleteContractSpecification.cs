using Dex.Specifications;

namespace BankSystem.App.Specifications.Contract;

public class UndeleteContractSpecification : Specification<Domain.Models.Contract>
{
    public UndeleteContractSpecification() : base(co => co.DeletedDate == null)
    {

    }
}

