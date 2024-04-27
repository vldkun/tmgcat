using tmgcat.Bll.Interfaces.Users;
using tmgcat.Bll.Models.Users;

namespace tmgcat.Bll.Services;

public class UserService : IUserService
{
    public Task<GetUserModel> GetUser(long userId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<GetUserModel[]> GetFriends(long[] gameIds, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<long> AddUser(AddUserModel user, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}