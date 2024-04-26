using tmgcat.Bll.Models.Games;

namespace tmgcat.Bll.Interfaces;

public interface IGameListRepository
{
    Task UpdateAsync(AddGameListItemModel game, CancellationToken token);
    Task AddAsync(AddGameListItemModel[] games, CancellationToken token);
    Task SetDeletedAsync(long userId, long gameId, CancellationToken token);
    Task<GameListItemModel[]> GetListAsync(long userId, CancellationToken token);
}