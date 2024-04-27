using tmgcat.Bll.Models.Movies;

namespace tmgcat.Bll.Interfaces.Movies;

public interface IMovieListRepository
{
    Task UpdateAsync(AddMovieListItemModel movie, CancellationToken token);
    Task AddAsync(AddMovieListItemModel[] movies, CancellationToken token);
    Task SetDeletedAsync(long userId, long movieId, CancellationToken token);
    Task<MovieListItemModel[]> GetListAsync(long userId, CancellationToken token);
}