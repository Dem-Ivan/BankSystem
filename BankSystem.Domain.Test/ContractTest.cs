using BankSystem.Domain.Models;
using Xunit;

namespace BankSystem.Domain.Tests
{
    public class ContractTest
    {
        [Fact]
        public void ContractRoutetest()
        {
            var counteragent = new Employee( 22, "Иван", role.ordinary_employee);




            var director = new Employee(45, "Эдуард Степанович", role.director);
           

            var contract = new Contract(role.director);

            contract.Сomplete(counteragent);
            contract.Sign(counteragent);
        }
    }
}
