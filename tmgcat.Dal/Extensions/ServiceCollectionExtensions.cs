using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using tmgcat.Bll.Interfaces.Comments;
using tmgcat.Bll.Interfaces.Games;
using tmgcat.Bll.Interfaces.Movies;
using tmgcat.Bll.Interfaces.TVShows;
using tmgcat.Bll.Interfaces.Users;
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
        services.AddScoped<IMovieListRepository, MovieListRepository>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<ITvShowListRepository, TvShowListRepository>();
        services.AddScoped<ITvShowRepository, TvShowRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
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