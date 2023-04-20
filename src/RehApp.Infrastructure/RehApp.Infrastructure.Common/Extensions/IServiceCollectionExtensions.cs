﻿using Microsoft.Extensions.DependencyInjection;
using RehApp.Infrastructure.Common.Interfaces;
using RehApp.Infrastructure.Common.Repository;
using System.Reflection;

namespace RehApp.Infrastructure.Common.Extensions;

public static class IServiceCollectionExtensions
{
    #region static fiels

    private static string IRepositoryName => typeof(IRepository<>).Name;
    private static string RepositoryName => typeof(Repository<>).Name;

    private static string ITranslatorName => typeof(ITranslator<,>).Name;

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

    public static IServiceCollection AddTranslators(this IServiceCollection services, Assembly assembly)
    {
        //getting all translators
        var translators = assembly.GetTypes()
            .Where(type => type?.GetInterface(ITranslatorName) is not null && !type.IsAbstract)
            .ToList();

        foreach (var translator in translators)
        {
            //getting the translator interface
            var contract = translator.GetInterface(ITranslatorName);

            //adding to dependency injection container
            services.AddScoped(contract!, translator);
        }

        return services;
    }
}
