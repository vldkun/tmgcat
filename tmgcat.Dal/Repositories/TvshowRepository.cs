using Microsoft.Extensions.Options;
using tmgcat.Bll.Interfaces.TVShows;
using tmgcat.Bll.Models.TVShows;
using tmgcat.Dal.Settings;

namespace tmgcat.Dal.Repositories;

public class TvShowRepository : PgRepository, ITvShowRepository
{
    public TvShowRepository(IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
    }

    public Task<GetTvShowModel> GetTvShowByIdAsync(long tvShowId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<GetTvShowTitleModel[]> GetTvShowTitleByIdAsync(long[] tvShowIds, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<long[]> AddAsync(TvShowModel[] tvShows, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}