using System.Net.Mail;

namespace Template.Infrastructure.MailService.Extensions;

internal static class StringExtensions
{
    internal static bool IsValidEmail(this string value)
    {
        if (string.IsNullOrEmpty(value)) return false;
        return MailAddress.TryCreate(value.Trim().ToLower(), out _);
    }
}
