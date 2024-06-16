using Microsoft.Extensions.DependencyInjection;
using tmgcat.Bll.Interfaces;
using tmgcat.Bll.Interfaces.Comments;
using tmgcat.Bll.Interfaces.Games;
using tmgcat.Bll.Interfaces.Movies;
using tmgcat.Bll.Interfaces.TVShows;
using tmgcat.Bll.Interfaces.Users;
using tmgcat.Bll.Services;

namespace tmgcat.Bll.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBllServices(
        this IServiceCollection services)
    {
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IGameListService, GameListService>();
        services.AddScoped<ITvShowListService, TvShowListService>();
        services.AddScoped<ITvShowService, TvShowService>();
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<IMovieListService, MovieListService>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}