using tmgcat.Bll.Enums;
using tmgcat.Bll.Interfaces.Comments;
using tmgcat.Bll.Interfaces.Games;
using tmgcat.Bll.Models.Comments;
using tmgcat.Bll.Models.Games;

namespace tmgcat.Bll.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    private readonly ICommentService _commentService;

    public GameService(IGameRepository gameRepository, ICommentService commentService)
    {
        _gameRepository = gameRepository;
        _commentService = commentService;
    }

    public async Task<GetGameModel> GetGame(long gameId, CancellationToken token)
    {
        return await _gameRepository.GetGameByIdAsync(gameId, token);
    }

    public async Task<GetGameTitleModel[]> GetGameTitles(long[] gameIds, CancellationToken token)
    {
        return await _gameRepository.GetGameTitleByIdAsync(gameIds, token);
    }

    public async Task<GetGameTitleModel[]> SearchGames(string query, CancellationToken token)
    {
        return await _gameRepository.SearchGamesAsync(query, token);
    }

    public async Task<long[]> AddGame(GameModel game, CancellationToken token)
    {
        return await _gameRepository.AddAsync(new[] { game }, token);
    }

    public async Task<GetCommentModel[]> GetComments(long gameId, CancellationToken token)
    {
        return await _commentService.GetComments(gameId, PageType.Game, token);
    }

    public async Task AddComment(AddCommentModel comment, CancellationToken token)
    {
        var newComment = new CommentModel()
        {
            Content = comment.Content,
            CreatedByUserId = comment.CreatedByUserId,
            ParentCommentId = comment.ParentCommentId,
            PageId = comment.PageId,
            PageType = (int)PageType.Game,
        };
        await _commentService.AddComment(newComment, token);
    }
}