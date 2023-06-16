using Notification.Exceptions;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Notification.Options;

namespace Notification;
internal class EmailSender : IEmailSender
{
    private readonly EmailOptions _options;
    private readonly int _defaultTimeout = 5000;

    public EmailSender(IOptions<EmailOptions> options)
    {
        _options = options.Value ?? throw new ArgumentNullException();
    }

    public async Task SendAsync(string subject, string body, string to, CancellationToken cancellationToken = default)
    {
        if (subject == null) throw new ArgumentNullException(nameof(subject));
        if (body == null) throw new ArgumentNullException(nameof(body));
        if (to == null) throw new ArgumentNullException(nameof(to));

        var fromAddress = new MailboxAddress(_options.From, _options.FromEmail);
        var toAddress = new MailboxAddress(to, to);

        await SendEmail(fromAddress, toAddress, subject, body, cancellationToken);
    }

    private async Task SendEmail(MailboxAddress fromAddress, MailboxAddress toAddress, string subject, string body, CancellationToken cancellationToken)
    {
        using var emailMessage = new MimeMessage();
        emailMessage.From.Add(fromAddress);
        emailMessage.To.Add(toAddress);
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart("html")
        {
            Text = body
        };

        try
        {
            using var client = new SmtpClient();
            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                cts.CancelAfter(Math.Max(_defaultTimeout, _options.ConnectTimeout));
                await client.ConnectAsync(_options.Host, _options.Port, SecureSocketOptions.Auto, cts.Token);                
                await client.AuthenticateAsync(_options.FromEmail, _options.Password, cts.Token);
            }

            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                cts.CancelAfter(Math.Max(_defaultTimeout, _options.SendTimeout));//TODO: SendTimeout в EmailOptions нету!
                await client.SendAsync(emailMessage, cts.Token);
                await client.DisconnectAsync(true, cts.Token);
            }
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            throw new TimeoutException(
                               $"Timeout [{_defaultTimeout}], when sending email to {toAddress}, server: {_options.Host}");
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            throw new EmailNotificationException("Failed to send E-Mail by SMPT.", ex);
        }
    }
}
