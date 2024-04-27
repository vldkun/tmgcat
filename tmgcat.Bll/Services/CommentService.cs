using tmgcat.Bll.Interfaces.Comments;
using tmgcat.Bll.Models.Comments;

namespace tmgcat.Bll.Services;

public class CommentService : ICommentService
{
    public Task<CommentModel[]> GetComment(string page, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task AddComment(CommentModel comment, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}