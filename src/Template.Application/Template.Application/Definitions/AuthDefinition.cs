using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.Text;
using Template.Infrastructure.Common.AppDefinition;
using Template.Infrastructure.Common.Attributes;
using Template.Infrastructure.Common.Models;

namespace Template.Application.Definitions;

[CallingOrder(2)]
public class AuthDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = Convert.ToBoolean(configuration["Authentication:Bearer:ValidateIssuer"]),
            ValidIssuer = configuration["Authentication:Bearer:Issuer"],
            ValidateAudience = Convert.ToBoolean(configuration["Authentication:Bearer:ValidateAudience"]),
            ValidAudience = configuration["Authentication:Bearer:Audience"],
            ValidateIssuerSigningKey = Convert.ToBoolean(configuration["Authentication:Bearer:ValidateSigningKey"]),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:Bearer:Key"]!)),
            ValidateLifetime = Convert.ToBoolean(configuration["Authentication:Bearer:ValidateLifetime"])
        };

        services
            .AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = "JWT_OR_COOKIE"; ;
                options.DefaultScheme = "JWT_OR_COOKIE";
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config =>
            {
                config.TokenValidationParameters = tokenValidationParameters;
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
            })
            .AddPolicyScheme("JWT_OR_COOKIE", "JWT_OR_COOKIE", options =>
            {
                options.ForwardDefaultSelector = context =>
                {
                    var header = (string?)context.Request.Headers[HeaderNames.Authorization];
                    if (header?.StartsWith(JwtBearerDefaults.AuthenticationScheme) ?? false)
                    {
                        return JwtBearerDefaults.AuthenticationScheme;
                    }

                    return IdentityConstants.ApplicationScheme;
                };
            }); ;

        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder(
                IdentityConstants.ApplicationScheme,
                JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
        });

        services.AddSingleton(tokenValidationParameters);

        var bearerSettings = new BearerSettings();
        configuration.Bind("Authentication:Bearer", bearerSettings);
        services.AddSingleton(bearerSettings);
    }

    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
