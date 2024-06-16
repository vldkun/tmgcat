using Dapper;
using Microsoft.Extensions.Options;
using tmgcat.Bll.Interfaces;
using tmgcat.Bll.Interfaces.Movies;
using tmgcat.Bll.Models.Movies;
using tmgcat.Dal.Entities;
using tmgcat.Dal.Settings;

namespace tmgcat.Dal.Repositories;

public class MovieListRepository : PgRepository, IMovieListRepository
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public MovieListRepository(IOptions<DalOptions> dalSettings, IDateTimeProvider dateTimeProvider) : base(dalSettings.Value)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task UpdateAsync(AddMovieListItemModel movie, CancellationToken token)
    {
        const string sqlQuery = @"
update movie_list
   set user_rating = @UserRating
     , status = @Status
 where user_id = @UserId
   and movie_id = @MovieId
";

        await using var connection = await GetConnection();
        await connection.ExecuteAsync(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    UserRating = movie.UserRating,
                    Status = movie.Status,
                    UserId = movie.UserId,
                    MovieId = movie.MovieId,
                },
                cancellationToken: token));
    }

    public async Task AddAsync(AddMovieListItemModel[] movies, CancellationToken token)
    {
        const string sqlQuery = @"
   insert into movie_list (user_id, movie_id, status, user_rating) 
   select user_id, movie_id, status, user_rating
     from UNNEST(@Movies)
returning id;
";

        await using var connection = await GetConnection();
        var ids = await connection.QueryAsync<long>(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    Movies = ConvertToEntity(movies)
                },
                cancellationToken: token));

        ids.ToArray();
    }

    public async Task SetDeletedAsync(long userId, long movieId, CancellationToken token)
    {
        const string sqlQuery = @"
update movie_list
   set deleted_at = @DeletedAt
 where user_id = @UserId
   and movie_id = @MovieId
";

        await using var connection = await GetConnection();
        await connection.ExecuteAsync(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    DeletedAt = _dateTimeProvider.GetCurrentTime(),
                    UserId = userId,
                    MovieId = movieId,
                },
                cancellationToken: token));
    }

    public async Task<MovieListItemModel[]> GetListAsync(long userId, CancellationToken token)
    {
        var baseSql = @"
   select ml.movie_id
        , m.title_en
        , m.title_ru
        , m.poster_path
        , ml.status
        , ml.user_rating
        , m.status as movie_status
     from movie_list as ml
left join movies m on m.id = ml.movie_id
    where ml.user_id = @UserId
      and ml.deleted_at is null
    order by status
";

        var cmd = new CommandDefinition(
            baseSql,
            new
            {
                UserId = userId
            },
            commandTimeout: DefaultTimeoutInSeconds,
            cancellationToken: token);

        await using var connection = await GetConnection();
        return (await connection.QueryAsync<MovieListItemModel>(cmd))
            .ToArray();
    }

    private MovieListDataEntity[] ConvertToEntity(AddMovieListItemModel[] movies)
    {
        return movies.Select(m => new MovieListDataEntity
        {
            UserId = m.UserId,
            MovieId = m.MovieId,
            Status = m.Status,
            UserRating = m.UserRating,
        }).ToArray();
    }
}