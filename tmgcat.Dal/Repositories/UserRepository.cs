using Dapper;
using Microsoft.Extensions.Options;
using tmgcat.Bll.Interfaces;
using tmgcat.Bll.Interfaces.Users;
using tmgcat.Bll.Models.Users;
using tmgcat.Dal.Settings;

namespace tmgcat.Dal.Repositories;

public class UserRepository : PgRepository, IUserRepository
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public UserRepository(IDateTimeProvider dateTimeProvider,
        IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<long> AddAsync(AddUserModel user, CancellationToken token)
    {
        const string sqlQuery = @"
   insert into users (name, email, password, created_at) 
   values (@Name, @Email, crypt(@Password, gen_salt('md5')), @CreatedAt)
returning id;
";

        await using var connection = await GetConnection();
        var id = await connection.QueryAsync<long>(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    Name = user.Name,
                    Password = user.Password,
                    Email = user.Email,
                    CreatedAt = _dateTimeProvider.GetCurrentTime()
                },
                cancellationToken: token));

        return id.ToArray().First();
    }

    public async Task<GetUserModel> GetAsync(long userId, CancellationToken token)
    {
        var sql = @"
select id, name, info, profile_picture_path, created_at
  from users 
 where id = @UserId";

        await using var connection = await GetConnection();
        return await connection.QuerySingleAsync<GetUserModel>(
            sql,
            new
            {
                UserId = userId
            });
    }

    public async Task<GetUserLightModel[]> GetFriendsAsync(long userId, CancellationToken token)
    {
        var baseSql = @"
   select fl.friend_id as id
        , u.name
        , u.profile_picture_path
     from friend_list as fl
left join users u on u.id = fl.friend_id
    where fl.user_id = @UserId
    order by u.name
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
        return (await connection.QueryAsync<GetUserLightModel>(cmd))
            .ToArray();
    }

    public async Task AddFriendAsync(long userId, long friendId, CancellationToken token)
    {
        const string sqlQuery = @"
   insert into friend_list (user_id, friend_id) 
   values (@UserId, @FriendId)
returning id;
";

        await using var connection = await GetConnection();
        var id = await connection.QueryAsync<long>(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    UserId = userId,
                    FriendId = friendId
                },
                cancellationToken: token));

        var res = id.ToArray().First();
    }

    public async Task<bool> AuthAsync(AddUserModel user, CancellationToken token)
    {
        const string sqlQuery = @"
   select (password = crypt(@Password, password)) AS password_match
     from users
    WHERE email = @Email;
";

        await using var connection = await GetConnection();
        var id = await connection.QueryAsync<bool>(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    Password = user.Password,
                    Email = user.Email,
                },
                cancellationToken: token));

        return id.ToArray().First();
    }

    public async Task<bool> IsUsernameExistAsync(string username, CancellationToken token)
    {
        const string sqlQuery = @"
   select count(1) > 0
     from users
    WHERE email = @Email;
";

        await using var connection = await GetConnection();
        var id = await connection.QueryAsync<bool>(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    Email = username,
                },
                cancellationToken: token));

        return id.ToArray().First();
    }

    public async Task<UserLogModel[]> GetUserLogAsync(long userId, CancellationToken token)
    {
        var baseSql = @"
       select title_type
            , title_id
            , user_id
            , content
            , created_at
         from user_logs
        where user_id = @UserId
        order by created_at desc
        limit 3;
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
        return (await connection.QueryAsync<UserLogModel>(cmd))
            .ToArray();
    }

    public async Task LogEventAsync(UserLogModel record, CancellationToken token)
    {
        const string sqlQuery = @"
   insert into user_logs (title_type, title_id, content, created_at, user_id) 
   values (@TitleType, @TitleId, @Content, @CreatedAt, @UserId)
returning id;
";

        await using var connection = await GetConnection();
        var id = await connection.QueryAsync<long>(
            new CommandDefinition(
                sqlQuery,
                new
                {
                    TitleType = record.TitleType,
                    TitleId = record.TitleId,
                    UserId = record.UserId,
                    Content = record.Content,
                    CreatedAt = _dateTimeProvider.GetCurrentTime()
                },
                cancellationToken: token));

        var res= id.ToArray().First();
    }
}