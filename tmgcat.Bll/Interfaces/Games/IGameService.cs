using tmgcat.Bll.Models.Games;

namespace tmgcat.Bll.Interfaces.Games;

public interface IGameService
{
    Task<GetGameModel> GetGame(long gameId, CancellationToken token);

    Task<GetGameTitleModel[]> GetGameTitles(long[] gameIds, CancellationToken token);

    Task<long[]> AddGame(GameModel game, CancellationToken token);

}