
namespace Contracts;
public class EmailMessageCommand : IMessageCommand
{
    public Guid RequestId { get; set; }
    public string Heading { get; set; }
    public string MessageText { get; set; }
    public string Email { get; set; }
    public int TimeToProcessing { get; set; }
}
