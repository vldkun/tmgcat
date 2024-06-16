using tmgcat.Bll.Models.Movies;

namespace tmgcat.Bll.Interfaces.Movies;

public interface IMovieRepository
{
    Task<GetMovieModel> GetMovieByIdAsync(long movieId, CancellationToken token);
    Task<GetMovieTitleModel[]> GetMovieTitleByIdAsync(long[] movieIds, CancellationToken token);
    Task<GetMovieTitleModel[]> SearchMoviesAsync(string query, CancellationToken token);
    Task<long[]> AddAsync(MovieModel[] movies, CancellationToken token);
}