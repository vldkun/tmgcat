using System.Transactions;
using tmgcat.Bll.Interfaces.Movies;
using tmgcat.Bll.Models.Movies;

namespace tmgcat.Bll.Services;

public class MovieListService : IMovieListService
{
    private readonly IMovieListRepository _movieListRepository;

    public MovieListService(IMovieListRepository movieListRepository)
    {
        _movieListRepository = movieListRepository;
    }

    public async Task<MovieListItemModel[]> GetList(long userId, CancellationToken token)
    {
        return await _movieListRepository.GetListAsync(userId, token);
    }

    public async Task AddToList(AddMovieListItemModel[] movies, CancellationToken token)
    {
        await _movieListRepository.AddAsync(movies, token);
    }

    public async Task ChangeUserRating(long userId, long movieId, int rating, CancellationToken token)
    {
        using var transaction = CreateTransactionScope(IsolationLevel.Serializable);
        var result = await _movieListRepository.GetListAsync(userId, token);
        if (result.Length != 0)
        {
            var item = result.First(p => p.MovieId == movieId);
            var newItem = new AddMovieListItemModel()
            {
                UserId = userId,
                MovieId = item.MovieId,
                Status = item.Status,
                UserRating = rating
            };
            await _movieListRepository.UpdateAsync(newItem, token);
        }

        transaction.Complete();
    }

    public async Task ChangeUserStatus(long userId, long movieId, int status, CancellationToken token)
    {
        using var transaction = CreateTransactionScope(IsolationLevel.Serializable);

        try
        {
            var result = await _movieListRepository.GetListAsync(userId, token);
            var item = result.First(p => p.MovieId == movieId);
            var newItem = new AddMovieListItemModel()
            {
                UserId = userId,
                MovieId = item.MovieId,
                Status = status,
                UserRating = item.UserRating
            };

            await _movieListRepository.UpdateAsync(newItem, token);
        }
        catch (InvalidOperationException e)
        {
            var newItem = new AddMovieListItemModel[]
            {
                new()
                {
                    UserId = userId,
                    MovieId = movieId,
                    Status = status,
                }
            };

            await _movieListRepository.AddAsync(newItem, token);
        }

        transaction.Complete();
    }

    public async Task<int> GetUserStatus(long userId, long movieId, CancellationToken token)
    {
        var list = await _movieListRepository.GetListAsync(userId, token);
        try
        {
            return list.Where(g => g.MovieId == movieId).Select(g => g.Status).First();
        }
        catch (InvalidOperationException e)
        {
            return -1;
        }
    }

    private TransactionScope CreateTransactionScope(
        IsolationLevel level = IsolationLevel.ReadCommitted)
    {
        return new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions
            {
                IsolationLevel = level,
                Timeout = TimeSpan.FromSeconds(5)
            },
            TransactionScopeAsyncFlowOption.Enabled);
    }
}