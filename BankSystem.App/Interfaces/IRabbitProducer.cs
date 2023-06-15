
namespace BankSystem.App.Interfaces;
public interface IRabbitProducer
{
    void SendMessage<T>(T messageContract) where T : class;   
}
