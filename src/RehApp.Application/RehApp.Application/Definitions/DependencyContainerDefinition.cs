using System.Reflection;
using RehApp.Data.EntityFrameworkCore;
using RehApp.Infrastructure.Common.AppDefinition;
using RehApp.Infrastructure.Common.Attributes;
using RehApp.Infrastructure.Common.Extensions;
using RehApp.Infrastructure.MailService;
using RehApp.Infrastructure.MailService.Models;

namespace RehApp.Application.Definitions;

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