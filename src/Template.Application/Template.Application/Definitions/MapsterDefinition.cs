using Mapster;
using MapsterMapper;
using System.Reflection;
using Template.Infrastructure.Common.AppDefinition;
using Template.Infrastructure.Common.Attributes;

namespace Template.Application.Definitions;

[CallingOrder(6)]
public class MapsterDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly()!);
        var mapperConfig = new Mapper(typeAdapterConfig);
        services.AddSingleton<IMapper>(mapperConfig);
    }
}