
namespace Notification;
public interface IEmailSender
{
    Task SendAsync(string subject, string body, string to, CancellationToken cancellationToken = default);
}
