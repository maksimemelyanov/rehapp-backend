using Template.Infrastructure.Common.AppDefinition;
using Template.Infrastructure.Common.Attributes;

namespace Template.Application.Definitions;

[CallingOrder(4)]
public class CorsDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var origins = configuration["Cors:Origins"]?.Split(',');

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowCredentials();

                if (origins is not { Length: > 0 }) return;

                if (origins.Contains("*"))
                {
                    builder.SetIsOriginAllowed(host => true);
                }
                else
                {
                    foreach (var origin in origins)
                    {
                        builder.WithOrigins(origin);
                    }
                }
            });
        });
    }

    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        app.UseCors();
    }
}
