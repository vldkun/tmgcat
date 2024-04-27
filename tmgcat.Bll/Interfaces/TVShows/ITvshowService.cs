using tmgcat.Bll.Models.TVShows;

namespace tmgcat.Bll.Interfaces.TVShows;

public interface ITvshowService
{
    Task<GetTvshowModel> GetTvshow(long tvshowId, CancellationToken token);

    Task<GetTvshowTitleModel[]> GetTvshowTitles(long[] tvshowIds, CancellationToken token);

    Task<long[]> AddTvshow(TvshowModel tvshow, CancellationToken token);
}