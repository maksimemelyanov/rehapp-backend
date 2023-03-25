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

        SeedCities(context);
        SeedDeveloperUser(userManager, context);

        userManager.Dispose();
        context.Dispose();
        scope.Dispose();
    }

    public static void SeedCities(ApplicationDbContext context)
    {
        var requiringInitialization = new List<City>()
        {
            new City { Name = "Екатеринбург", ShortName = "ekb", Latitude = 56.833332, Longitude = 60.583332 },
            new City { Name = "Москва", ShortName = "msk", Latitude = 55.751244, Longitude = 37.618423 },
        };

        var existing = context.Cities.ToList();

        var missingCities = requiringInitialization
            .Where(x => !existing.Any(y => y.Name == x.Name))
            .ToList();

        context.Cities.AddRange(missingCities);
        context.SaveChanges();
    }

    public static void SeedDeveloperUser(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        if (userManager.FindByNameAsync("developer").Result is not null) return;

        var user = new ApplicationUser
        {
            UserName = "developer",
            Email = "developer@rehapp.ru",
            FirstName = "Superuser",
            LastName = "Developer",
            EmailConfirmed = true,
            City = context.Cities.FirstOrDefault()!
        };

        userManager.CreateAsync(user, "qwerty123").Wait();
        userManager.AddClaimAsync(user, new Claim("Id", user.Id.ToString())).Wait();
    }
}
