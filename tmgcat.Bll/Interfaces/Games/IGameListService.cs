using tmgcat.Bll.Models.Games;

namespace tmgcat.Bll.Interfaces.Games;

public interface IGameListService
{
    Task<GameListItemModel[]> GetList(long userId, CancellationToken token);
    Task AddToList(AddGameListItemModel[] games, CancellationToken token);
    Task ChangeUserRating(long userId, long gameId, int rating, CancellationToken token);
    Task ChangePlayingTime(long userId, long gameId, int time, CancellationToken token);
    Task ChangeUserStatus(long userId, long gameId, int status, CancellationToken token);
    Task<int> GetUserStatus(long userId, long gameId, CancellationToken token);

}