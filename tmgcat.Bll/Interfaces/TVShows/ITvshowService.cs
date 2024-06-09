using tmgcat.Bll.Models.TVShows;

namespace tmgcat.Bll.Interfaces.TVShows;

public interface ITvShowService
{
    Task<GetTvShowModel> GetTvShow(long tvShowId, CancellationToken token);

    Task<GetTvShowTitleModel[]> GetTvShowTitles(long[] tvShowIds, CancellationToken token);

    Task<long[]> AddTvShow(TvShowModel tvShow, CancellationToken token);
}