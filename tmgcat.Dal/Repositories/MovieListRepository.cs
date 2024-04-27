using Microsoft.Extensions.Options;
using tmgcat.Bll.Interfaces.Movies;
using tmgcat.Bll.Models.Movies;
using tmgcat.Dal.Settings;

namespace tmgcat.Dal.Repositories;

public class MovieListRepository : PgRepository, IMovieListRepository
{
    public MovieListRepository(IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
    }

    public Task UpdateAsync(AddMovieListItemModel movie, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(AddMovieListItemModel[] movies, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task SetDeletedAsync(long userId, long movieId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<MovieListItemModel[]> GetListAsync(long userId, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}