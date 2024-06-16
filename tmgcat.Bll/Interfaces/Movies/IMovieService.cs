using tmgcat.Bll.Models.Comments;
using tmgcat.Bll.Models.Movies;

namespace tmgcat.Bll.Interfaces.Movies;

public interface IMovieService
{
    Task<GetMovieModel> GetMovie(long movieId, CancellationToken token);
    Task<GetMovieTitleModel[]> GetMovieTitles(long[] movieIds, CancellationToken token);
    Task<long[]> AddMovie(MovieModel movie, CancellationToken token);
    Task<GetCommentModel[]> GetComments(long movieId, CancellationToken token);
    Task AddComment(AddCommentModel comment, CancellationToken token);
    Task<GetMovieTitleModel[]> SearchMovies(string query, CancellationToken token);
}