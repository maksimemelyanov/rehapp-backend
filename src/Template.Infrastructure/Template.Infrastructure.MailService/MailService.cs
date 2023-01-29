using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using Template.Infrastructure.MailService.Models;

namespace Template.Infrastructure.MailService;

public class MailService : IMailService
{
    private readonly MailSettings settings;

    public MailService(MailSettings settings)
    {
        this.settings = settings;
    }

    public async Task SendAsync(MailMessage message, CancellationToken cancellationToken)
    {
        var mimeMessage = GetMimeMessage(message.RecipientMail, message.Subject, message.Body, message.TextFormat);
        await SendUsingSmtpAsync(mimeMessage, cancellationToken);
    }

    private MimeMessage GetMimeMessage(string recipientMail, string subject, string body, TextFormat textFormat)
    {
        var mimeMessage = new MimeMessage()
        {
            Subject = subject,
            Body = new TextPart(textFormat) { Text = body }
        };

        mimeMessage.From.Add(MailboxAddress.Parse(settings.SenderMail));
        mimeMessage.To.Add(MailboxAddress.Parse(recipientMail));

        return mimeMessage;
    }

    private async Task SendUsingSmtpAsync(MimeMessage message, CancellationToken cancellationToken)
    {
        using var smtp = new SmtpClient();

        await smtp.ConnectAsync(settings.Host, settings.Port, settings.SecureSocketOptions, cancellationToken);
        await smtp.AuthenticateAsync(settings.Login, settings.Password, cancellationToken);
        await smtp.SendAsync(message, cancellationToken);
        await smtp.DisconnectAsync(true, cancellationToken);
    }
}
