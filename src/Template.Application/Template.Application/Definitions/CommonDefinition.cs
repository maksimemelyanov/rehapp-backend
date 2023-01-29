using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Template.Infrastructure.Common.AppDefinition;
using Template.Infrastructure.Common.Attributes;

namespace Template.Application.Definitions;

[CallingOrder(0)]
public class InitialCommonDefinition : AppDefinition
{
    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        app.UseRouting();
    }
}

[CallingOrder(11)]
public class CommonDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        services.AddLocalization();
        services.AddHttpContextAccessor();
        services.AddResponseCaching();
        services.AddMemoryCache();

        services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
            });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });
    }

    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });
        app.UseHsts();
        app.UseHttpsRedirection();
        app.MapDefaultControllerRoute();
        app.UseResponseCaching();

        app.UseStaticFiles();
        app.MapFallbackToFile("index.html");
    }
}