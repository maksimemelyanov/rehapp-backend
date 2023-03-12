using MediatR;
using RehApp.Infrastructure.Common.AppDefinition;
using RehApp.Infrastructure.Common.Attributes;
using System.Reflection;

namespace RehApp.Application.Definitions;

[CallingOrder(7)]
public class MediatRDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}
