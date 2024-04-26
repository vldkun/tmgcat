using tmgcat.Bll.Models.Users;

namespace tmgcat.Bll.Interfaces;

public interface IUserRepository
{
    Task<long> AddAsync(AddUserModel user, CancellationToken token);
    Task<GetUserModel> GetAsync(long userId, CancellationToken token);
}