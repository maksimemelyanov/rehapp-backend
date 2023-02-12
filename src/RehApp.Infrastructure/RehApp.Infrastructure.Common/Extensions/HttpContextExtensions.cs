using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace RehApp.Infrastructure.Common.Extensions;

public static class HttpContextExtensions
{
    public static string? GetUserId(this HttpContext httpContext)
    {
        return httpContext.User.Claims.GetByClaimType(ClaimTypes.NameIdentifier);
    }
}
