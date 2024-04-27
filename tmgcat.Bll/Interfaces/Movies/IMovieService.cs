using tmgcat.Bll.Models.Movies;

namespace tmgcat.Bll.Interfaces.Movies;

public interface IMovieService
{
    Task<GetMovieModel> GetMovie(long movieId, CancellationToken token);

    Task<GetMovieTitleModel[]> GetMovieTitles(long[] movieIds, CancellationToken token);

    Task<long[]> AddMovie(MovieModel movie, CancellationToken token);
}