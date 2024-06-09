using tmgcat.Bll.Enums;
using tmgcat.Bll.Models.Comments;
using tmgcat.Bll.Models.Games;

namespace tmgcat.Bll.Interfaces.Games;

public interface IGameService
{
    Task<GetGameModel> GetGame(long gameId, CancellationToken token);

    Task<GetGameTitleModel[]> GetGameTitles(long[] gameIds, CancellationToken token);

    Task<GetGameTitleModel[]> SearchGames(string query, CancellationToken token);

    Task<long[]> AddGame(GameModel game, CancellationToken token);

    Task<GetCommentModel[]> GetComments(long gameId, CancellationToken token);

    Task AddComment(AddCommentModel comment, CancellationToken token);

}