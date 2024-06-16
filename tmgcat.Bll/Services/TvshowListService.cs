using System.Transactions;
using tmgcat.Bll.Interfaces.TVShows;
using tmgcat.Bll.Models.TVShows;

namespace tmgcat.Bll.Services;

public class TvShowListService : ITvShowListService
{
    private readonly ITvShowListRepository _tvShowListRepository;

    public TvShowListService(ITvShowListRepository tvShowListRepository)
    {
        _tvShowListRepository = tvShowListRepository;
    }

    public async Task<TvShowListItemModel[]> GetList(long userId, CancellationToken token)
    {
        return await _tvShowListRepository.GetListAsync(userId, token);
    }

    public async Task AddToList(AddTvShowListItemModel[] tvShows, CancellationToken token)
    {
        await _tvShowListRepository.AddAsync(tvShows, token);
    }

    public async Task ChangeUserRating(long userId, long tvShowId, int rating, CancellationToken token)
    {
        using var transaction = CreateTransactionScope(IsolationLevel.Serializable);
        var result = await _tvShowListRepository.GetListAsync(userId, token);
        if (result.Length != 0)
        {
            var item = result.First(p => p.TvShowId == tvShowId);
            var newItem = new AddTvShowListItemModel()
            {
                UserId = userId,
                TvShowId = item.TvShowId,
                EpisodesWatched = item.EpisodesWatched,
                Status = item.Status,
                UserRating = rating
            };
            await _tvShowListRepository.UpdateAsync(newItem, token);
        }

        transaction.Complete();
    }

    public async Task ChangeEpisodesNumber(long userId, long tvShowId, int episodes, CancellationToken token)
    {
        using var transaction = CreateTransactionScope(IsolationLevel.Serializable);
        var result = await _tvShowListRepository.GetListAsync(userId, token);
        if (result.Length != 0)
        {
            var item = result.First(p => p.TvShowId == tvShowId);
            var newItem = new AddTvShowListItemModel()
            {
                UserId = userId,
                TvShowId = item.TvShowId,
                EpisodesWatched = episodes,
                Status = item.Status,
                UserRating = item.UserRating
            };
            await _tvShowListRepository.UpdateAsync(newItem, token);
        }

        transaction.Complete();
    }

    public async Task ChangeUserStatus(long userId, long tvShowId, int status, CancellationToken token)
    {
        using var transaction = CreateTransactionScope(IsolationLevel.Serializable);

        try
        {
            var result = await _tvShowListRepository.GetListAsync(userId, token);
            var item = result.First(p => p.TvShowId == tvShowId);
            var newItem = new AddTvShowListItemModel
            {
                UserId = userId,
                TvShowId = item.TvShowId,
                Status = status,
                EpisodesWatched = item.EpisodesWatched,
                UserRating = item.UserRating
            };

            await _tvShowListRepository.UpdateAsync(newItem, token);
        }
        catch (InvalidOperationException e)
        {
            var newItem = new AddTvShowListItemModel[]
            {
                new()
                {
                    UserId = userId,
                    TvShowId = tvShowId,
                    Status = status,
                    EpisodesWatched = 0
                }
            };

            await _tvShowListRepository.AddAsync(newItem, token);
        }

        transaction.Complete();
    }

    public async Task<int> GetUserStatus(long userId, long tvShowId, CancellationToken token)
    {
        var list = await _tvShowListRepository.GetListAsync(userId, token);
        try
        {
            return list.Where(g => g.TvShowId == tvShowId).Select(g => g.Status).First();
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