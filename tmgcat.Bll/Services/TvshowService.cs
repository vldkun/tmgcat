using tmgcat.Bll.Interfaces.TVShows;
using tmgcat.Bll.Models.TVShows;

namespace tmgcat.Bll.Services;

public class TvShowService : ITvShowService
{
    public Task<GetTvShowModel> GetTvShow(long tvShowId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<GetTvShowTitleModel[]> GetTvShowTitles(long[] tvShowIds, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<long[]> AddTvShow(TvShowModel tvShow, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}