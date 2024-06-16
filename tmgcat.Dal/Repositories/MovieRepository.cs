using Dapper;
using Microsoft.Extensions.Options;
using tmgcat.Bll.Interfaces.Movies;
using tmgcat.Bll.Models.Movies;
using tmgcat.Bll.Models.TVShows;
using tmgcat.Dal.Settings;

namespace tmgcat.Dal.Repositories;

public class MovieRepository : PgRepository, IMovieRepository
{
    public MovieRepository(IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
    }

    public async Task<GetMovieModel> GetMovieByIdAsync(long movieId, CancellationToken token)
    {
        var sql = @"
select id, title_ru, title_en, description, creators, imdb_id, tmdb_id, runtime_minutes
     , released_at, genres, poster_path, status, imdb_rating, kp_rating
     , (SELECT AVG(user_rating)
          FROM movie_list
         WHERE movie_id = @MovieId) as user_rating
  from movies 
 where id = @MovieId
";

        await using var connection = await GetConnection();
        return await connection.QuerySingleAsync<GetMovieModel>(
            sql,
            new
            {
                MovieId = movieId
            });
    }

    public Task<GetMovieTitleModel[]> GetMovieTitleByIdAsync(long[] movieIds, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<GetMovieTitleModel[]> SearchMoviesAsync(string query, CancellationToken token)
    {
        const string sqlQuery = @"
SELECT id
     , title_en
     , title_ru
     , released_at
     , poster_path
     , greatest(public.similarity(title_en, @Query::text), public.similarity(title_ru, @Query::text)) AS similarity
  FROM movies
 ORDER BY similarity DESC
 limit 5;
";
        var cmd = new CommandDefinition(
            sqlQuery,
            new
            {
                Query = query
            },
            commandTimeout: DefaultTimeoutInSeconds,
            cancellationToken: token);

        await using var connection = await GetConnection();
        return (await connection.QueryAsync<GetMovieTitleModel>(cmd))
            .ToArray();
    }

    public Task<long[]> AddAsync(MovieModel[] movies, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}