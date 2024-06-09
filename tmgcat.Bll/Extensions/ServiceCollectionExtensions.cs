using Microsoft.Extensions.DependencyInjection;
using tmgcat.Bll.Interfaces;
using tmgcat.Bll.Interfaces.Comments;
using tmgcat.Bll.Interfaces.Games;
using tmgcat.Bll.Services;

namespace tmgcat.Bll.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBllServices(
        this IServiceCollection services)
    {
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IGameListService, GameListService>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<ICommentService, CommentService>();

        return services;
    }
}