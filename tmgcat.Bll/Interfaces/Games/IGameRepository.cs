using tmgcat.Bll.Models.Games;

namespace tmgcat.Bll.Interfaces.Games;

public interface IGameRepository
{
    Task<GetGameModel> GetGameByIdAsync(long gameId, CancellationToken token);
    Task<GetGameTitleModel[]> GetGameTitleByIdAsync(long[] gameIds, CancellationToken token);
    Task<GetGameTitleModel[]> SearchGamesAsync(string query, CancellationToken token);
    Task<long[]> AddAsync(GameModel[] games, CancellationToken token);
}