namespace BankSystem.Domain.Models;

public class ContractHistoryElement
{
    public Guid Id { get; } = Guid.NewGuid();

    public Guid ContractId { get; }
    public Contract Contract { get; set; }

    public DateTime ChangeDate { get; }

    public Status NewStatus { init; get; }

    public ContractHistoryElement()
    {
        ChangeDate = DateTime.UtcNow;
    }

    public ContractHistoryElement(Contract currentContract)
    {
        ContractId = currentContract.Id;
        Contract = currentContract;
        NewStatus = currentContract.Status;
        ChangeDate = DateTime.UtcNow;
    }
}