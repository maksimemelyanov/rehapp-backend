using RehApp.Data.EntityFrameworkCore;
using RehApp.Infrastructure.Common.AppDefinition;
using RehApp.Infrastructure.Common.Attributes;

namespace RehApp.Application.Definitions;

[CallingOrder(9)]
public class DataSeedingDefinition : AppDefinition
{
    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        DatabaseInitializer.Run(app.Services);
    }
}
