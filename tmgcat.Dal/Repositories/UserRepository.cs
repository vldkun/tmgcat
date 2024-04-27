using Microsoft.Extensions.Options;
using tmgcat.Bll.Interfaces.Users;
using tmgcat.Bll.Models.Users;
using tmgcat.Dal.Settings;

namespace tmgcat.Dal.Repositories;

public class UserRepository : PgRepository, IUserRepository
{
    public UserRepository(
        IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
    }

    public async Task<long> AddAsync(AddUserModel user, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<GetUserModel> GetAsync(long userId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<long[]> GetFriendIdsAsync(long userId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task AddFriendAsync(long userId, long friendId, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}