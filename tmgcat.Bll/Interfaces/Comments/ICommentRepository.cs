using tmgcat.Bll.Models.Comments;

namespace tmgcat.Bll.Interfaces.Comments;

public interface ICommentRepository
{
    Task<CommentModel[]> Get(string page, CancellationToken token);

    Task Add(CommentModel comment, CancellationToken token);
}
