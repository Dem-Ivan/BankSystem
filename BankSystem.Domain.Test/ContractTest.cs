using BankSystem.Domain.Models;
using Xunit;

namespace BankSystem.Domain.Tests
{
    public class ContractTest
    {
        [Fact]
        public void ContractRoutetest()
        {
            var counteragent = new Employee
            {
                Name = "Иван",
                Age = 22,
                Role = Role.ordinary_employee
            };

            var director = new Employee
            {
                Name = "Эдуард Степанович",
                Age = 45,
                Role = Role.director
            };

            var contract = new Contract();

            contract.Сomplete(counteragent);
            contract.Sign(counteragent);
        }
    }
}
