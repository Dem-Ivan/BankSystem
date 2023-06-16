
namespace Contracts;
public interface IMessageCommand
{
    public Guid RequestId { get; set; }
    public int TimeToProcessing { get; set; }
}
