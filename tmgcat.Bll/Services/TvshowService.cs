using tmgcat.Bll.Interfaces.TVShows;
using tmgcat.Bll.Models.TVShows;

namespace tmgcat.Bll.Services;

public class TvshowService : ITvshowService
{
    public Task<GetTvshowModel> GetTvshow(long tvshowId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<GetTvshowTitleModel[]> GetTvshowTitles(long[] tvshowIds, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<long[]> AddTvshow(TvshowModel tvshow, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}