using System.Transactions;
using tmgcat.Bll.Interfaces;
using tmgcat.Bll.Models.Games;

namespace tmgcat.Bll.Services;

public class GameListService : IGameListService
{
    private readonly IGameListRepository _gameListRepository;

    public GameListService(IGameListRepository gameListRepository)
    {
        _gameListRepository = gameListRepository;
    }
    
    public async Task<GameListItemModel[]> GetList(long userId, CancellationToken token)
    {
        return await _gameListRepository.GetListAsync(userId, token);
    }

    public async Task AddToList(AddGameListItemModel[] games, CancellationToken token)
    {
        await _gameListRepository.AddAsync(games, token);
    }

    public async Task ChangeUserRating(long userId, long gameId, int rating, CancellationToken token)
    {
        using var transaction = CreateTransactionScope(IsolationLevel.Serializable);

        var item = _gameListRepository.GetListAsync(userId,token).Result.First(p => p.GameId==gameId);
        var newItem = new AddGameListItemModel
        {
            UserId = userId,
            GameId = item.GameId,
            Status = item.Status,
            MinutesPlayed = item.MinutesPlayed,
            UserRating = rating
        };
        await _gameListRepository.UpdateAsync(newItem, token);
        transaction.Complete();
    }

    public async Task ChangePlayingTime(long userId, long gameId, int time, CancellationToken token)
    {
        using var transaction = CreateTransactionScope(IsolationLevel.Serializable);

        var item = _gameListRepository.GetListAsync(userId, token).Result.First(p => p.GameId == gameId);
        var newItem = new AddGameListItemModel
        {
            UserId = userId,
            GameId = item.GameId,
            Status = item.Status,
            MinutesPlayed = time,
            UserRating = item.UserRating
        };
        await _gameListRepository.UpdateAsync(newItem, token);
        transaction.Complete();
    }

    public async Task ChangeUserStatus(long userId, long gameId, int status, CancellationToken token)
    {
        using var transaction = CreateTransactionScope(IsolationLevel.Serializable);

        var item = _gameListRepository.GetListAsync(userId, token).Result.First(p => p.GameId == gameId);
        var newItem = new AddGameListItemModel
        {
            UserId = userId,
            GameId = item.GameId,
            Status = status,
            MinutesPlayed = item.MinutesPlayed,
            UserRating = item.UserRating
        };
        await _gameListRepository.UpdateAsync(newItem, token);
        transaction.Complete();
    }

    private TransactionScope CreateTransactionScope(
        IsolationLevel level = IsolationLevel.ReadCommitted)
    {
        return new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions
            {
                IsolationLevel = level,
                Timeout = TimeSpan.FromSeconds(5)
            },
            TransactionScopeAsyncFlowOption.Enabled);
    }
}