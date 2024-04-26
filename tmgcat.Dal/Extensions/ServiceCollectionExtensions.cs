using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using tmgcat.Bll.Interfaces;
using tmgcat.Dal.Infrastructure;
using tmgcat.Dal.Repositories;
using tmgcat.Dal.Settings;

namespace tmgcat.Dal.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDalRepositories(
        this IServiceCollection services)
    {
        AddPostgresRepositories(services);
        return services;
    }
    private static void AddPostgresRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGameListRepository, GameListRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
    }

    public static IServiceCollection AddDalInfrastructure(
        this IServiceCollection services)
    {
        //configure postrges types
        Postgres.MapCompositeTypes();
        
        //add migrations
        Postgres.AddMigrations(services);
        
        return services;

    }
}