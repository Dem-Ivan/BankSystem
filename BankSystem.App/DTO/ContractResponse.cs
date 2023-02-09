using BankSystem.Domain.Models;

namespace BankSystem.App.DTO;

public class ContractResponse
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public Status Status { get; set; }
    public string Body { get; set; }
}