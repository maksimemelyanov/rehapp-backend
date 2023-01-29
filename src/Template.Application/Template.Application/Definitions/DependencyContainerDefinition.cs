using System.Reflection;
using Template.Data.EntityFrameworkCore;
using Template.Infrastructure.Common.AppDefinition;
using Template.Infrastructure.Common.Attributes;
using Template.Infrastructure.Common.Extensions;
using Template.Infrastructure.MailService;
using Template.Infrastructure.MailService.Models;

namespace Template.Application.Definitions;

[CallingOrder(10)]
public class DependencyContainerDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories(Assembly.GetAssembly(typeof(ApplicationDbContext))!);

        services.AddSingleton(configuration);

        var mailSettings = new MailSettings();
        configuration.Bind(nameof(MailSettings), mailSettings);
        services.AddSingleton(mailSettings);
        services.AddScoped<IMailService, MailService>();
    }
}