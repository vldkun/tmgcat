
using tmgcat.Bll.Models.TVShows;

namespace tmgcat.Bll.Interfaces.TVShows;

public interface ITvShowListRepository
{
    Task UpdateAsync(AddTvShowListItemModel tvShow, CancellationToken token);
    Task AddAsync(AddTvShowListItemModel[] tvShows, CancellationToken token);
    Task SetDeletedAsync(long userId, long tvShowId, CancellationToken token);
    Task<TvShowListItemModel[]> GetListAsync(long userId, CancellationToken token);
}