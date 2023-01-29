using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Template.Infrastructure.Common.Repository;

namespace Template.Infrastructure.Common.Extensions;

public static class IServiceCollectionExtensions
{
    #region static fiels

    private static string IRepositoryName => typeof(IRepository<>).Name;
    private static string RepositoryName => typeof(Repository<>).Name;

    #endregion

    public static IServiceCollection AddRepositories(this IServiceCollection services, Assembly assembly)
    {
        //getting all repositories
        var repositories = assembly.GetTypes()
            .Where(type => type?.BaseType?.Name == RepositoryName)
            .ToList();

        foreach (var repository in repositories)
        {
            //getting the repository interface
            var contract = repository.GetInterfaces()
                .Where(x => x.GetInterface(IRepositoryName) is not null)
                .SingleOrDefault();

            //skip it if the repository does not inherit the desired interface
            if (contract is null) continue;

            //adding to dependency injection container
            services.AddScoped(contract, repository);
        }

        return services;
    }
}
