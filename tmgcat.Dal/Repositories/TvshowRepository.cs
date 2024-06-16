using Dapper;
using Microsoft.Extensions.Options;
using tmgcat.Bll.Interfaces.TVShows;
using tmgcat.Bll.Models.Games;
using tmgcat.Bll.Models.TVShows;
using tmgcat.Dal.Settings;

namespace tmgcat.Dal.Repositories;

public class TvShowRepository : PgRepository, ITvShowRepository
{
    public TvShowRepository(IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
    }

    public async Task<GetTvShowModel> GetTvShowByIdAsync(long tvShowId, CancellationToken token)
    {
        var sql = @"
select id, title_ru, title_en, description, creators, imdb_id, tmdb_id, avg_ep_runtime, episodes_number, seasons_number
     , first_air_date, last_air_date, genres, poster_path, status, imdb_rating, kp_rating
     , (SELECT AVG(user_rating)
          FROM tvshow_list
         WHERE tvshow_id = @TvShowId) as user_rating
  from tvshows 
 where id = @TvShowId
";

        await using var connection = await GetConnection();
        return await connection.QuerySingleAsync<GetTvShowModel>(
            sql,
            new
            {
                TvShowId = tvShowId
            });
    }

    public Task<GetTvShowTitleModel[]> GetTvShowTitleByIdAsync(long[] tvShowIds, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<long[]> AddAsync(TvShowModel[] tvShows, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<GetTvShowTitleModel[]> SearchTvShowsAsync(string query, CancellationToken token)
    {
        const string sqlQuery = @"
SELECT id
     , title_en
     , title_ru
     , first_air_date
     , last_air_date
     , poster_path
     , greatest(public.similarity(title_en, @Query::text), public.similarity(title_ru, @Query::text)) AS similarity
  FROM tvshows
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
        return (await connection.QueryAsync<GetTvShowTitleModel>(cmd))
            .ToArray();
    }
}