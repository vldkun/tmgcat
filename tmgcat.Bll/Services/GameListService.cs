using System.Transactions;
using tmgcat.Bll.Interfaces.Games;
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
        var result = await _gameListRepository.GetListAsync(userId, token);
        if (result.Length != 0)
        {
            var item = result.First(p => p.GameId == gameId);
            var newItem = new AddGameListItemModel
            {
                UserId = userId,
                GameId = item.GameId,
                Status = item.Status,
                MinutesPlayed = item.MinutesPlayed,
                UserRating = rating
            };
            await _gameListRepository.UpdateAsync(newItem, token);
        }

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

        try
        {
            var result = await _gameListRepository.GetListAsync(userId, token);
            var item = result.First(p => p.GameId == gameId);
            var newItem = new AddGameListItemModel
            {
                UserId = userId,
                GameId = item.GameId,
                Status = status,
                MinutesPlayed = item.MinutesPlayed,
                UserRating = item.UserRating
            };

            await _gameListRepository.UpdateAsync(newItem, token);
        }
        catch (InvalidOperationException e)
        {
            var newItem = new AddGameListItemModel[]
            {
                new()
                {
                    UserId = userId,
                    GameId = gameId,
                    Status = status,
                    MinutesPlayed = 0
                }
            };

            await _gameListRepository.AddAsync(newItem, token);
        }

        transaction.Complete();
    }

    public async Task<int> GetUserStatus(long userId, long gameId, CancellationToken token)
    {
        var list = await _gameListRepository.GetListAsync(userId, token);
        try
        {
            return list.Where(g => g.GameId == gameId).Select(g => g.Status).First();
        }
        catch (InvalidOperationException e)
        {
            return -1;
        }
        
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