using Microsoft.Extensions.Options;
using tmgcat.Bll.Interfaces.TVShows;
using tmgcat.Bll.Models.TVShows;
using tmgcat.Dal.Settings;

namespace tmgcat.Dal.Repositories;

public class TvshowRepository : PgRepository, ITvshowRepository
{
    public TvshowRepository(IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
    }

    public Task<GetTvshowModel> GetTvshowByIdAsync(long tvshowId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<GetTvshowTitleModel[]> GetTvshowTitleByIdAsync(long[] tvshowIds, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<long[]> AddAsync(TvshowModel[] tvshows, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}