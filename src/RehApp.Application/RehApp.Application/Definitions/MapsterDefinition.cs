﻿using Mapster;
using MapsterMapper;
using RehApp.Infrastructure.Common.AppDefinition;
using RehApp.Infrastructure.Common.Attributes;
using System.Reflection;

namespace RehApp.Application.Definitions;

[CallingOrder(6)]
public class MapsterDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;

        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly()!);
        typeAdapterConfig.Scan(Assembly.GetAssembly(typeof(DescriptionTypeConst))!);

        var mapperConfig = new Mapper(typeAdapterConfig);
        services.AddSingleton<IMapper>(mapperConfig);
    }
}