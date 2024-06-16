using Dapper;
using Microsoft.Extensions.Options;
using tmgcat.Bll.Interfaces;
using tmgcat.Bll.Interfaces.TVShows;
using tmgcat.Bll.Models.TVShows;
using tmgcat.Dal.Entities;
using tmgcat.Dal.Settings;

namespace tmgcat.Dal.Repositories;

public class TvShowListRepository : PgRepository, ITvShowListRepository
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public TvShowListRepository(IOptions<DalOptions> dalSettings, IDateTimeProvider dateTimeProvider) : base(dalSettings.Value)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task UpdateAsync(AddTvShowListItemModel tvShow, CancellationToken token)
    {
        const string sqlQuery = @"
update movie_list
   set user_rating = @UserRating
     , status = @Status
     , episodes_watched = @EpisodesWatched
 where user_id = @UserId
   and tvshow_id = @TvShowId
";

        await using var connection = await GetConnection();
        await connection.ExecuteAsync(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    UserRating = tvShow.UserRating,
                    Status = tvShow.Status,
                    UserId = tvShow.UserId,
                    TvShowId = tvShow.TvShowId,
                    EpisodesWatched = tvShow.EpisodesWatched,
                },
                cancellationToken: token));
    }

    public async Task AddAsync(AddTvShowListItemModel[] tvShows, CancellationToken token)
    {
        const string sqlQuery = @"
   insert into tvshow_list (user_id, tvshow_id, status, episodes_watched, user_rating) 
   select user_id, tvshow_id, status, episodes_watched, user_rating
     from UNNEST(@TvShows)
returning id;
";

        await using var connection = await GetConnection();
        var ids = await connection.QueryAsync<long>(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    TvShows = ConvertToEntity(tvShows)
                },
                cancellationToken: token));

        ids.ToArray();
    }

    public async Task SetDeletedAsync(long userId, long tvShowId, CancellationToken token)
    {
        const string sqlQuery = @"
update tvshow_list
   set deleted_at = @DeletedAt
 where user_id = @UserId
   and tvshow_id = @TvShowId
";

        await using var connection = await GetConnection();
        await connection.ExecuteAsync(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    DeletedAt = _dateTimeProvider.GetCurrentTime(),
                    UserId = userId,
                    TvShowId = tvShowId,
                },
                cancellationToken: token));
    }

    public async Task<TvShowListItemModel[]> GetListAsync(long userId, CancellationToken token)
    {
        var baseSql = @"
   select tl.tvshow_id
        , t.title_en
        , t.title_ru
        , t.poster_path
        , tl.status
        , tl.user_rating
        , t.status as tvshow_status
     from tvshow_list as tl
left join tvshows t on t.id = tl.tvshow_id
    where tl.user_id = @UserId
      and tl.deleted_at is null
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
        return (await connection.QueryAsync<TvShowListItemModel>(cmd))
            .ToArray();
    }

    private TvShowListDataEntity[] ConvertToEntity(AddTvShowListItemModel[] games)
    {
        return games.Select(t => new TvShowListDataEntity
        {
            UserId = t.UserId,
            TvShowId = t.TvShowId,
            Status = t.Status,
            EpisodesWatched = t.EpisodesWatched,
            UserRating = t.UserRating,
        }).ToArray();
    }
}