using tmgcat.Bll.Enums;
using tmgcat.Bll.Models.Comments;

namespace tmgcat.Bll.Interfaces.Comments;

public interface ICommentService
{
    Task<GetCommentModel[]> GetComments(long pageId, PageType pageType, CancellationToken token);

    Task AddComment(CommentModel comment, CancellationToken token);
}