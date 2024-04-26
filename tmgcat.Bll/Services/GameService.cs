using tmgcat.Bll.Interfaces;
using tmgcat.Bll.Models.Games;

namespace tmgcat.Bll.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;
    
    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }
    public async Task<GetGameModel> GetGame(long gameId, CancellationToken token)
    {
        return await _gameRepository.GetGameByIdAsync(gameId,token);
    }

    public async Task<GetGameTitleModel[]> GetGameTitles(long[] gameIds, CancellationToken token)
    {
        return await _gameRepository.GetGameTitleByIdAsync(gameIds, token);
    }

    public async Task<long[]> AddGame(GameModel game, CancellationToken token)
    {
        return await _gameRepository.AddAsync(new[]{game}, token);
    }
}