using tmgcat.Bll.Enums;
using tmgcat.Bll.Interfaces.Comments;
using tmgcat.Bll.Interfaces.Users;
using tmgcat.Bll.Models.Comments;
using tmgcat.Bll.Models.Users;

namespace tmgcat.Bll.Services;

public class UserService : IUserService
{
    private readonly ICommentService _commentService;
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, ICommentService commentService)
    {
        _userRepository = userRepository;
        _commentService = commentService;
    }

    public async Task<GetUserModel> GetUser(long userId, CancellationToken token)
    {
        return await _userRepository.GetAsync(userId, token);
    }

    public async Task<GetUserLightModel[]> GetFriends(long userId, CancellationToken token)
    {
        return await _userRepository.GetFriendsAsync(userId, token);
    }

    public async Task<long> AddUser(AddUserModel user, CancellationToken token)
    {
        return await _userRepository.AddAsync(user, token);
    }

    public async Task AddFriend(long userId, long friendId, CancellationToken token)
    {
        await _userRepository.AddFriendAsync(userId, friendId, token);
    }

    public async Task<GetCommentModel[]> GetComments(long userId, CancellationToken token)
    {
        return await _commentService.GetComments(userId, PageType.User, token);
    }

    public async Task AddComment(AddCommentModel comment, CancellationToken token)
    {
        var newComment = new CommentModel()
        {
            Content = comment.Content,
            CreatedByUserId = comment.CreatedByUserId,
            ParentCommentId = comment.ParentCommentId,
            PageId = comment.PageId,
            PageType = (int)PageType.User,
        };
        await _commentService.AddComment(newComment, token);
    }
}