using BankSystem.Domain.Models;
using BankSystem.Domain.Models.Templates;
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
            var template = ContractTemplate.GetInstance();
            template.SignerRole = role.director;

            //Act
            var contract = template.GetNewContract(); // 1)создан           
            contract.Сomplete(counteragent); //2)запонен
            contract.SendforAcquaintance(); //3) отправлен на ознакомление
            contract.Cquaint(counteragent); //4) контрагент подтвердил (ознакомился) новый статус "на подписание"
            contract.Sign(signer); //5) подписан 

            //Assert
            Assert.Equal(5, contract.History.Count);
        }
    }
}
