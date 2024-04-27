using Microsoft.Extensions.Options;
using tmgcat.Bll.Interfaces.Movies;
using tmgcat.Bll.Models.Movies;
using tmgcat.Dal.Settings;

namespace tmgcat.Dal.Repositories;

public class MovieRepository : PgRepository, IMovieRepository
{
    public MovieRepository(IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
    }

    public Task<GetMovieModel> GetMovieByIdAsync(long movieId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<GetMovieTitleModel[]> GetMovieTitleByIdAsync(long[] movieIds, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<long[]> AddAsync(MovieModel[] movies, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}