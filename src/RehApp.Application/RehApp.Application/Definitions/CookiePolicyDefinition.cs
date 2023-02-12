using RehApp.Infrastructure.Common.AppDefinition;
using RehApp.Infrastructure.Common.Attributes;

namespace RehApp.Application.Definitions;

[CallingOrder(3)]
public class CookiePolicyDefinition : AppDefinition
{
    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        app.UseCookiePolicy(new CookiePolicyOptions()
        {
            OnAppendCookie = options =>
            {
                options.CookieOptions.SameSite = SameSiteMode.None;
                options.CookieOptions.Secure = true;
            }
        });
    }
}
