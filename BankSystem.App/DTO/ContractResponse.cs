using System;
using BankSystem.Domain.Models;

namespace BankSystem.App.DTO
{
    public class ContractResponse
    {
        private Guid _contractId { get; }
        private Status status { get; }
        private string body { get; }
        private Employee author { get; }
    }
}
