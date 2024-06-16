using tmgcat.Bll.Models.Users;

namespace tmgcat.Bll.Interfaces.Users;

public interface IUserRepository
{
    Task<long> AddAsync(AddUserModel user, CancellationToken token);
    Task<GetUserModel> GetAsync(long userId, CancellationToken token);
    Task<GetUserLightModel[]> GetFriendsAsync(long userId, CancellationToken token);
    Task AddFriendAsync(long userId, long friendId, CancellationToken token);
    Task<bool> AuthAsync(AddUserModel user, CancellationToken token);
    Task<bool> IsUsernameExistAsync(string username, CancellationToken token);
    Task<UserLogModel[]> GetUserLogAsync(long userId, CancellationToken token);
    Task LogEventAsync(UserLogModel record, CancellationToken token);
}