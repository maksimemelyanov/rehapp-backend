using MediatR;
using System.Reflection;
using Template.Infrastructure.Common.AppDefinition;
using Template.Infrastructure.Common.Attributes;

namespace Template.Application.Definitions;

[CallingOrder(7)]
public class MediatRDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}
