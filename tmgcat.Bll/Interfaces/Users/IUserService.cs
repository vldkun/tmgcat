using tmgcat.Bll.Models.Comments;
using tmgcat.Bll.Models.Users;

namespace tmgcat.Bll.Interfaces.Users;

public interface IUserService
{
    Task<GetUserModel> GetUser(long userId, CancellationToken token);

    Task<GetUserLightModel[]> GetFriends(long userId, CancellationToken token);

    Task<long> AddUser(AddUserModel user, CancellationToken token);

    Task AddFriend(long userId, long friendId, CancellationToken token);

    Task<GetCommentModel[]> GetComments(long userId, CancellationToken token);

    Task AddComment(AddCommentModel comment, CancellationToken token);
}