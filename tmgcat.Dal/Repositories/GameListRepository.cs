using Dapper;
using Microsoft.Extensions.Options;
using tmgcat.Bll.Interfaces;
using tmgcat.Bll.Models.Games;
using tmgcat.Dal.Entities;
using tmgcat.Dal.Settings;

namespace tmgcat.Dal.Repositories;

public class GameListRepository : PgRepository, IGameListRepository
{
    private IDateTimeProvider _dateTimeProvider;

    public GameListRepository(IDateTimeProvider dateTimeProvider,
        IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task UpdateAsync(AddGameListItemModel game, CancellationToken token)
    {
        const string sqlQuery = @"
update game_list
   set user_rating = @UserRating
     , status = @Status
     , minutes_played = @MinutesPlayed
 where user_id = @UserId
   and game_id = @GameId
";

        await using var connection = await GetConnection();
        await connection.ExecuteAsync(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    UserRating = game.UserRating,
                    MinutesPlayed = game.MinutesPlayed,
                    Status = game.Status,
                    UserId = game.UserId,
                    GameId = game.GameId,
                },
                cancellationToken: token));
    }

    public async Task AddAsync(AddGameListItemModel[] games, CancellationToken token)
    {
        const string sqlQuery = @"
insert into game_list (user_id, game_id, status, minutes_played, user_rating) 
select user_id, game_id, status, minutes_played, user_rating
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

        ids.ToArray();
    }

    public async Task SetDeletedAsync(long userId, long gameId, CancellationToken token)
    {
        const string sqlQuery = @"
update game_list
   set deleted_at = @DeletedAt
 where user_id = @UserId
   and game_id = @GameId
";

        await using var connection = await GetConnection();
        await connection.ExecuteAsync(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    DeletedAt = _dateTimeProvider.GetCurrentTime(),
                    UserId = userId,
                    GameId = gameId,
                },
                cancellationToken: token));
    }

    public async Task<GameListItemModel[]> GetListAsync(long userId, CancellationToken token)
    {
        var baseSql = @"
   select gl.game_id
        , g.title
        , g.cover_path
        , gl.status
        , gl.minutes_played
        , gl.user_rating
     from game_list as gl
left join games g on g.id = gl.game_id
    where gl.user_id = @UserId
      and gl.deleted_at is null
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
        return (await connection.QueryAsync<GameListItemModel>(cmd))
            .ToArray();
    }

    private GameListDataEntity[] ConvertToEntity(AddGameListItemModel[] games)
    {
        return games.Select(g => new GameListDataEntity
        {
            UserId = g.UserId,
            GameId = g.GameId,
            Status = g.Status,
            MinutesPlayed = g.MinutesPlayed,
            UserRating = g.UserRating,
        }).ToArray();
    }
}