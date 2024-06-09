using Dapper;
using Microsoft.Extensions.Options;
using tmgcat.Bll.Interfaces.Games;
using tmgcat.Bll.Models.Games;
using tmgcat.Dal.Entities;
using tmgcat.Dal.Settings;

namespace tmgcat.Dal.Repositories;

public class GameRepository : PgRepository, IGameRepository
{
    public GameRepository(
        IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
    }

    public async Task<GetGameModel> GetGameByIdAsync(long gameId, CancellationToken token)
    {
        var sql = @"
select id, title, description, igdb_id, released_at, platforms, genres, cover_path, status, category, involved_companies
  from games 
 where id = @GameId";

        await using var connection = await GetConnection();
        return await connection.QuerySingleAsync<GetGameModel>(
            sql,
            new
            {
                GameId = gameId
            });
    }

    public async Task<GetGameTitleModel[]> GetGameTitleByIdAsync(long[] gameIds, CancellationToken token)
    {
        var baseSql = @"
select id, title, released_at, cover_path
  from games
";

        var conditions = new List<string>();
        var @params = new DynamicParameters();

        if (gameIds.Any())
        {
            conditions.Add($"id = ANY(@GameIds)");
            @params.Add($"GameIds", gameIds);
        }

        var cmd = new CommandDefinition(
            baseSql + $" WHERE {string.Join(" AND ", conditions)} ",
            @params,
            commandTimeout: DefaultTimeoutInSeconds,
            cancellationToken: token);

        await using var connection = await GetConnection();
        return (await connection.QueryAsync<GetGameTitleModel>(cmd))
            .ToArray();
    }

    public async Task<GetGameTitleModel[]> SearchGamesAsync(string query, CancellationToken token)
    {
        const string sqlQuery = @"
SELECT id
     , title
     , released_at
     , cover_path
     , public.similarity(title, @Query::text) AS similarity
  FROM games
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
        return (await connection.QueryAsync<GetGameTitleModel>(cmd))
            .ToArray();
    }

    public async Task<long[]> AddAsync(GameModel[] games, CancellationToken token)
    {
        const string sqlQuery = @"
   insert into games (title, description, igdb_id, released_at, platforms, genres, cover_path, status, category, involved_companies) 
   select title, description, igdb_id, released_at, platforms, genres, cover_path, status, category, involved_companies
     from UNNEST(@Games)
returning id;
";

        await using var connection = await GetConnection();
        var ids = await connection.QueryAsync<long>(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    Games = ConvertToEntity(games)
                },
                cancellationToken: token));

        return ids
            .ToArray();
    }

    private GameDataEntity[] ConvertToEntity(GameModel[] games)
    {
        return games.Select(g => new GameDataEntity
        {
            Title = g.Title,
            Description = g.Description,
            IgdbId = g.IgdbId,
            ReleasedAt = g.ReleasedAt,
            Platforms = g.Platforms,
            CoverPath = g.CoverPath,
            Status = g.Status,
            Genres = g.Genres,
            Category = g.Category,
            InvolvedCompanies = g.InvolvedCompanies
        }).ToArray();
    }
}