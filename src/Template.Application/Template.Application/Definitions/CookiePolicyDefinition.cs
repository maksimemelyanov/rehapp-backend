using Template.Infrastructure.Common.AppDefinition;
using Template.Infrastructure.Common.Attributes;

namespace Template.Application.Definitions;

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
