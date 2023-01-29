using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Template.Infrastructure.Common.Extensions;

public static class HttpContextExtensions
{
    public static string? GetUserId(this HttpContext httpContext)
    {
        return httpContext.User.Claims.GetByClaimType(ClaimTypes.NameIdentifier);
    }
}
