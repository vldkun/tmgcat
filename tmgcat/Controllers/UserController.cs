using Microsoft.AspNetCore.Mvc;
using tmgcat.App.Contracts;
using tmgcat.Bll.Interfaces.Users;
using tmgcat.Bll.Models.Comments;

namespace tmgcat.App.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet]
    [Route("{userId}/Comments")]
    public async Task<ActionResult<CommentDto[]>> GetComments(long userId)
    {
        var comments = await _userService.GetComments(userId, CancellationToken.None);
        var result = comments.Select(c => new CommentDto
        {
            Content = c.Content,
            CreatedByUserId = c.CreatedByUserId,
            CreatedAt = c.CreatedAt,
            Id = c.Id,
            Level = c.Level,
            PageId = c.PageId,
            PageType = c.PageType,
            ParentCommentId = c.ParentCommentId,
            RootCommentId = c.RootCommentId,
            UserName = c.UserName,
            UserProfilePicturePath = c.UserProfilePicturePath
        });
        return Ok(result);
    }

    [HttpGet]
    [Route("{userId}/")]
    public async Task<ActionResult<UserDto>> GetUser(long userId)
    {
        var user = await _userService.GetUser(userId, CancellationToken.None);
        var result = new UserDto
        {
            UserId = user.Id,
            Info = user.Info,
            CreatedAt = user.CreatedAt,
            Name = user.Name,
            ProfilePicturePath = user.ProfilePicturePath
        };
        return Ok(result);
    }

    [HttpGet]
    [Route("{userId}/Friends")]
    public async Task<ActionResult<UserDto[]>> GetFriends(long userId)
    {
        var users = await _userService.GetFriends(userId, CancellationToken.None);
        var result = users.Select(user => new UserDto
        {
            UserId = user.Id,
            Name = user.Name,
            ProfilePicturePath = user.ProfilePicturePath
        });
        return Ok(result);
    }

    [HttpPost]
    [Route("{userId}/Comments")]
    public async Task<ActionResult> AddComment(long userId, string content, long createdByUserId,
        long? parentCommentId)
    {
        var comment = new AddCommentModel()
        {
            Content = content,
            CreatedByUserId = createdByUserId,
            ParentCommentId = parentCommentId,
            PageId = userId
        };
        await _userService.AddComment(comment, CancellationToken.None);
        return Ok();
    }

    [HttpPost]
    [Route("{userId}/Friend")]
    public async Task<ActionResult> AddFriend(long userId, long friendId)
    {
        await _userService.AddFriend(userId, friendId, CancellationToken.None);
        return Ok();
    }
}