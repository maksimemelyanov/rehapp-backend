using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RehApp.Domain.RelationalDatabase.Entities;
using RehApp.Domain.RelationalDatabase.Enums;
using RehApp.Infrastructure.Common.Extensions;

namespace RehApp.Application.Extensions;

public static class UserManagerExtensions
{
    public static async Task<ApplicationUser?> FindByExternalLoginInfoAsync(
        this UserManager<ApplicationUser> userManager,
        AuthProvider provider,
        string externalId)
    {
        var user = await userManager.Users
            .Where(x => x.ExtAuthInfo
                .Any(y => y.ExternalId == externalId && y.Provider == provider))
            .SingleOrDefaultAsync();

        return user ?? await userManager.FindByNameAsync($"{provider}-{externalId}");
    }

    public static async Task<UserLoginInfo?> FindUserLoginInfoAsync(
        this UserManager<ApplicationUser> userManager,
        ApplicationUser user,
        string loginProvider,
        string providerkey)
    {
        var collection = await userManager.GetLoginsAsync(user);

        var login = collection
            .Where(x => x.LoginProvider == loginProvider && x.ProviderKey == providerkey)
            .SingleOrDefault();

        return login;
    }

    public static async Task<ApplicationUser?> GetCurrentUserAsync(
        this UserManager<ApplicationUser> userManager,
        HttpContext httpContext)
    {
        var id = httpContext.GetUserId();
        return id is null ? default : await userManager.Users.SingleOrDefaultAsync(x => x.Id == id);
    }
}
