using MailKit.Security;
using System;

namespace RehApp.Infrastructure.MailService.Models;

public class MailSettings
{
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public SecureSocketOptions SecureSocketOptions { get; set; }
    public string SenderMail { get; set; } = null!;

    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;

    public MailSettings(SecureSocketOptions secureSocketOptions = SecureSocketOptions.Auto)
    {
        SecureSocketOptions = secureSocketOptions;
    }
}
