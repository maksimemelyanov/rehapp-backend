using MimeKit.Text;
using RehApp.Infrastructure.MailService.Extensions;

namespace RehApp.Infrastructure.MailService.Models;

public class MailMessage
{
    public string RecipientMail { get; set; }
    public string Subject { get; set; }
    public TextFormat TextFormat { get; set; }
    public string Body { get; set; }

    public MailMessage(string recipientMail, string subject, string body, TextFormat textFormat)
    {
        if (string.IsNullOrEmpty(recipientMail))
        {
            throw new ArgumentNullException("The recipient's email address is not specified.");
        }

        if (string.IsNullOrEmpty(subject))
        {
            throw new ArgumentNullException("The subject of the letter is not specified.");
        }

        if (!recipientMail.IsValidEmail())
        {
            throw new ArgumentException("Invalid recipient's email is specified.");
        }

        RecipientMail = recipientMail;
        Subject = subject;
        TextFormat = textFormat;
        Body = body;
    }
}