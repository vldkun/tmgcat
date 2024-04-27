using tmgcat.Bll.Interfaces.Movies;
using tmgcat.Bll.Models.Movies;

namespace tmgcat.Bll.Services;

public class MovieService : IMovieService
{
    public Task<GetMovieModel> GetMovie(long movieId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<GetMovieTitleModel[]> GetMovieTitles(long[] movieIds, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<long[]> AddMovie(MovieModel movie, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}