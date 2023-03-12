using Microsoft.AspNetCore.Http;

namespace RehApp.Infrastructure.Common.Extensions;

public static class HttpContextExtensions
{
    public static Guid? GetUserId(this HttpContext httpContext)
    {
        var result = Guid.TryParse(httpContext.User.Claims.GetByClaimType("Id"), out Guid id);
        return result ? id : null;
    }
}
