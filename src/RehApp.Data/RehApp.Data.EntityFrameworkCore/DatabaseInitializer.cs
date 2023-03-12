using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RehApp.Domain.RelationalDatabase.Entities;
using System.Security.Claims;

namespace RehApp.Data.EntityFrameworkCore;

//WARNING: don't create async methods
public static class DatabaseInitializer
{
    public static void Run(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        using var context = scope.ServiceProvider.GetService<ApplicationDbContext>()!;
        using var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>()!;

        SeedDeveloperUser(userManager);

        userManager.Dispose();
        context.Dispose();
        scope.Dispose();
    }

    public static void SeedDeveloperUser(UserManager<ApplicationUser> userManager)
    {
        if (userManager.FindByNameAsync("developer").Result is not null) return;

        var user = new ApplicationUser
        {
            UserName = "developer",
            Email = "developer@rehapp.ru",
            EmailConfirmed = true
        };

        userManager.CreateAsync(user, "qwerty123").Wait();
        userManager.AddClaimAsync(user, new Claim("Id", user.Id.ToString())).Wait();
    }
}
