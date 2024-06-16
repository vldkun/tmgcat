using tmgcat.Bll.Models.Movies;

namespace tmgcat.Bll.Interfaces.Movies;

public interface IMovieListService
{
    Task<MovieListItemModel[]> GetList(long userId, CancellationToken token);
    Task AddToList(AddMovieListItemModel[] movies, CancellationToken token);
    Task ChangeUserRating(long userId, long movieId, int rating, CancellationToken token);
    Task ChangeUserStatus(long userId, long movieId, int status, CancellationToken token);
    Task<int> GetUserStatus(long userId, long movieId, CancellationToken token);
}