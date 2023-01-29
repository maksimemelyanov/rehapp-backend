using Template.Infrastructure.MailService.Models;

namespace Template.Infrastructure.MailService;

public interface IMailService
{
    public Task SendAsync(MailMessage message, CancellationToken cancellationToken);
}
