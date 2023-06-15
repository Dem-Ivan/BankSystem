
namespace Contracts;
public class EmailMessageCommand
{
    public Guid RequestId { get; set; }
    public string Heading { get; set; }
    public string MessageText { get; set; }
    public string Email { get; set; }
}
