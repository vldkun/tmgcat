using tmgcat.Bll.Interfaces.Movies;
using tmgcat.Bll.Models.Movies;

namespace tmgcat.Bll.Services;

public class MovieListService : IMovieListService
{
    public Task<MovieListItemModel[]> GetList(long userId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task AddToList(AddMovieListItemModel[] movies, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task ChangeUserRating(long userId, long movieId, int rating, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task ChangeUserStatus(long userId, long movieId, int status, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}