namespace RehApp.Infrastructure.MailService.Constants;

public static class EmailTemplates
{
    public static string PasswordResetTemplate(string confirmationLink)
    {
        return @$"Для смены пароля перейдите по <a href=""{confirmationLink}"">ссылке</a>";
    }
}
