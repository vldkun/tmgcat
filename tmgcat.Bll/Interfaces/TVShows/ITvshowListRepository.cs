
using tmgcat.Bll.Models.TVShows;

namespace tmgcat.Bll.Interfaces.TVShows;

public interface ITvshowListRepository
{
    Task UpdateAsync(AddTvshowListItemModel tvshow, CancellationToken token);
    Task AddAsync(AddTvshowListItemModel[] tvshows, CancellationToken token);
    Task SetDeletedAsync(long userId, long tvshowId, CancellationToken token);
    Task<TvshowListItemModel[]> GetListAsync(long userId, CancellationToken token);
}