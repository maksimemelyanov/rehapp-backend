using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using RehApp.Infrastructure.Common;
using RehApp.Infrastructure.Common.AppDefinition;
using RehApp.Infrastructure.Common.Attributes;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

namespace RehApp.Application.Definitions;

[CallingOrder(8)]
public class SwaggerDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = Constants.ServiceName,
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            options.AddEnumsWithValuesFixFilters();
        });
    }

    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment environment)
    {
        app.UseSwagger();
        app.UseSwaggerUI(settings =>
        {
            settings.DocumentTitle = $"{Constants.ServiceName}";
            settings.DefaultModelExpandDepth(0);
            settings.DefaultModelRendering(ModelRendering.Model);
            settings.DocExpansion(DocExpansion.None);
            settings.DisplayRequestDuration();
        });
    }
}