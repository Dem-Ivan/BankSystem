using BankSystem.Domain.Models;
using BankSystem.Domain.Models.Templates;

namespace BankSystem.Domain.Tests;

public class ContractTest
{
    [Fact]
    public void ContractRouteTest()
    {
        //Arrange

        var counteragent = new Client { Age = 22, Name = "Иван" };
        var bankOperator = new Employee(33, "Петрова", Role.OrdinaryEmployee);
        var signer = new Employee(45, "Эдуард Степанович", Role.Director);
        var template = ContractTemplate.GetInstance();
        template.SignerRole = Role.Director;

        //Act
        var contract = template.GetNewContract(bankOperator, counteragent); // 1)создан
        contract.Сomplete(counteragent); //2)запонен
        contract.SendforAcquaintance(); //3) отправлен на ознакомление
        contract.Cquaint(counteragent); //4) контрагент подтвердил (ознакомился) новый статус "на подписание"
        contract.Sign(signer); //5) подписан

        //contract.History.RemoveAt(0);
        //Assert
        Assert.Equal(5, contract.History.Count);
    }
}