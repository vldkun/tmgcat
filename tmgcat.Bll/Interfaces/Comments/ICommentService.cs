using tmgcat.Bll.Models.Comments;

namespace tmgcat.Bll.Interfaces.Comments;

public interface ICommentService
{
    Task<CommentModel[]> GetComment(string page, CancellationToken token);

    Task AddComment(CommentModel comment, CancellationToken token);
}