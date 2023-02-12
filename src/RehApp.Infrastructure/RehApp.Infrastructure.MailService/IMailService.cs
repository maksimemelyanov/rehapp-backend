using RehApp.Infrastructure.MailService.Models;

namespace RehApp.Infrastructure.MailService;

public interface IMailService
{
    public Task SendAsync(MailMessage message, CancellationToken cancellationToken);
}
