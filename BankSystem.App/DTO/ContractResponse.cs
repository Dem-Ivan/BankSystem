using System;
using BankSystem.Domain.Models;

namespace BankSystem.App.DTO
{
    public class ContractResponse
    {
        public Guid Id { get; }
        public Status Status { get; }
        public string Body { get; }
        public Employee Author { get; }
    }
}
