using tmgcat.Bll.Enums;
using tmgcat.Bll.Models.Comments;

namespace tmgcat.Bll.Interfaces.Comments;

public interface ICommentRepository
{
    Task<GetCommentModel[]> GetAsync(long pageId, PageType pageType, CancellationToken token);

    Task AddAsync(CommentModel comment, CancellationToken token);
}
