using tmgcat.Bll.Models.Users;

namespace tmgcat.Bll.Interfaces.Users;

public interface IUserService
{
    Task<GetUserModel> GetUser(long userId, CancellationToken token);

    Task<GetUserModel[]> GetFriends(long[] gameIds, CancellationToken token);

    Task<long> AddUser(AddUserModel user, CancellationToken token);
}