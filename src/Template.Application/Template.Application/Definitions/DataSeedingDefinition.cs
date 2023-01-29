using Template.Data.EntityFrameworkCore;
using Template.Infrastructure.Common.AppDefinition;
using Template.Infrastructure.Common.Attributes;

namespace Template.Application.Definitions;

[CallingOrder(9)]
public class DataSeedingDefinition : AppDefinition
{
    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        DatabaseInitializer.Run(app.Services);
    }
}
