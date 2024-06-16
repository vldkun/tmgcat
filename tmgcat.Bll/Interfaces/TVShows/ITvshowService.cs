using tmgcat.Bll.Models.Comments;
using tmgcat.Bll.Models.TVShows;

namespace tmgcat.Bll.Interfaces.TVShows;

public interface ITvShowService
{
    Task<GetTvShowModel> GetTvShow(long tvShowId, CancellationToken token);
    Task<GetTvShowTitleModel[]> GetTvShowTitles(long[] tvShowIds, CancellationToken token);
    Task<long[]> AddTvShow(TvShowModel tvShow, CancellationToken token);
    Task<GetCommentModel[]> GetComments(long tvShowId, CancellationToken token);
    Task AddComment(AddCommentModel comment, CancellationToken token);
    Task<GetTvShowTitleModel[]> SearchTvShows(string query, CancellationToken token);
}