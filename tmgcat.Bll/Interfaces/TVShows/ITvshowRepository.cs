using tmgcat.Bll.Models.TVShows;

namespace tmgcat.Bll.Interfaces.TVShows;

public interface ITvshowRepository
{
    Task<GetTvshowModel> GetTvshowByIdAsync(long tvshowId, CancellationToken token);
    Task<GetTvshowTitleModel[]> GetTvshowTitleByIdAsync(long[] tvshowIds, CancellationToken token);
    Task<long[]> AddAsync(TvshowModel[] tvshows, CancellationToken token);
}