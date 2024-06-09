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
}