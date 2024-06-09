using Microsoft.Extensions.Options;
using tmgcat.Bll.Interfaces.TVShows;
using tmgcat.Bll.Models.TVShows;
using tmgcat.Dal.Settings;

namespace tmgcat.Dal.Repositories;

public class TvShowListRepository : PgRepository, ITvShowListRepository
{
    public TvShowListRepository(IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
    }

    public Task UpdateAsync(AddTvShowListItemModel tvShow, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(AddTvShowListItemModel[] tvShows, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task SetDeletedAsync(long userId, long tvShowId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<TvShowListItemModel[]> GetListAsync(long userId, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}