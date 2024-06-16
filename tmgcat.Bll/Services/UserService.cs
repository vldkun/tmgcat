using tmgcat.Bll.Enums;
using tmgcat.Bll.Interfaces.Comments;
using tmgcat.Bll.Interfaces.Games;
using tmgcat.Bll.Interfaces.Movies;
using tmgcat.Bll.Interfaces.TVShows;
using tmgcat.Bll.Interfaces.Users;
using tmgcat.Bll.Models.Comments;
using tmgcat.Bll.Models.Users;

namespace tmgcat.Bll.Services;

public class UserService : IUserService
{
    private readonly ICommentService _commentService;
    private readonly IUserRepository _userRepository;
    private readonly IMovieService _movieService;
    private readonly ITvShowService _tvShowService;
    private readonly IGameService _gameService;

    public UserService(
        IUserRepository userRepository,
        ICommentService commentService,
        IMovieService movieService,
        ITvShowService tvShowService,
        IGameService gameService)
    {
        _userRepository = userRepository;
        _commentService = commentService;
        _movieService = movieService;
        _tvShowService = tvShowService;
        _gameService = gameService;
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

    public async Task<bool> AuthUser(AddUserModel user, CancellationToken token)
    {
        var isExist = await _userRepository.IsUsernameExistAsync(user.Email, token);
        if (isExist)
        {
            return await _userRepository.AuthAsync(user, token);
        }

        return false;
    }

    public async Task<bool> CheckUsernameIsExist(string username, CancellationToken token)
    {
        return await _userRepository.IsUsernameExistAsync(username, token);
    }

    public async Task<UserLogModel[]> GetLogs(long userId, CancellationToken token)
    {
        var logs = await _userRepository.GetUserLogAsync(userId, token);
        foreach (var item in logs)
        {
            switch (item.TitleType)
            {
                case 1:
                    var movie = await _movieService.GetMovie(item.TitleId, token);
                    item.PosterPath = movie.PosterPath;
                    item.Title = movie.TitleRu;
                    break;
                case 2:
                    var tvShow = await _tvShowService.GetTvShow(item.TitleId, token);
                    item.PosterPath = tvShow.PosterPath;
                    item.Title = tvShow.TitleRu;
                    break;
                case 3:
                    var game = await _gameService.GetGame(item.TitleId, token);
                    item.PosterPath = game.CoverPath;
                    item.Title = game.Title;
                    break;
            }
        }
        return logs;
    }
}