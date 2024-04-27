using Microsoft.Extensions.Options;
using tmgcat.Bll.Interfaces.TVShows;
using tmgcat.Bll.Models.TVShows;
using tmgcat.Dal.Settings;

namespace tmgcat.Dal.Repositories;

public class TvshowListRepository : PgRepository, ITvshowListRepository
{
    public TvshowListRepository(IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
    }

    public Task UpdateAsync(AddTvshowListItemModel tvshow, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(AddTvshowListItemModel[] tvshows, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task SetDeletedAsync(long userId, long tvshowId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<TvshowListItemModel[]> GetListAsync(long userId, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}