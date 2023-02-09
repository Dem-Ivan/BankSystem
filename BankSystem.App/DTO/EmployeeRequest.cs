
using BankSystem.Domain.Models;

namespace BankSystem.App.DTO;

public class EmployeeRequest
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Role Role { get; set; }
}