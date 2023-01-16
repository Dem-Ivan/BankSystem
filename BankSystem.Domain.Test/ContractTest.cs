using BankSystem.Domain.Models;
using Xunit;

namespace BankSystem.Domain.Tests
{
    public class ContractTest
    {
        [Fact]
        public void ContractRouteTest()
        {
            //Arrange

            var counteragent = new Client(22, "Иван");           
            var signer = new Employee(45, "Эдуард Степанович", role.director);

            //Act
            var contract = new Contract(role.director); // 1)создан           
            contract.Сomplete(counteragent); //2)запонен
            contract.SendforAcquaintance(counteragent); //3) отправлен на ознакомление
            contract.Cquaint(counteragent); //4) контрагент подтвердил (ознакомился) новый статус "на подписание"
            contract.Sign(signer); //5) подписан 

            //Assert
            Assert.Equal(5, contract.History.Count);
        }
    }
}
