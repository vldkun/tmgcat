using tmgcat.Bll.Enums;
using tmgcat.Bll.Interfaces.Comments;
using tmgcat.Bll.Models.Comments;

namespace tmgcat.Bll.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<GetCommentModel[]> GetComments(long pageId, PageType pageType, CancellationToken token)
    {
        return await _commentRepository.GetAsync(pageId, pageType, token);
    }

    public async Task AddComment(CommentModel comment, CancellationToken token)
    {
        await _commentRepository.AddAsync(comment, token);
    }
}