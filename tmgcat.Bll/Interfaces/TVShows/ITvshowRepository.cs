using tmgcat.Bll.Models.TVShows;

namespace tmgcat.Bll.Interfaces.TVShows;

public interface ITvShowRepository
{
    Task<GetTvShowModel> GetTvShowByIdAsync(long tvShowId, CancellationToken token);
    Task<GetTvShowTitleModel[]> GetTvShowTitleByIdAsync(long[] tvShowIds, CancellationToken token);
    Task<long[]> AddAsync(TvShowModel[] tvShows, CancellationToken token);
}