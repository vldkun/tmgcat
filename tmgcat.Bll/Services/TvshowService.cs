using tmgcat.Bll.Enums;
using tmgcat.Bll.Interfaces.Comments;
using tmgcat.Bll.Interfaces.TVShows;
using tmgcat.Bll.Models.Comments;
using tmgcat.Bll.Models.TVShows;

namespace tmgcat.Bll.Services;

public class TvShowService : ITvShowService
{
    private readonly ITvShowRepository _tvShowRepository;
    private readonly ICommentService _commentService;

    public TvShowService(ITvShowRepository tvShowRepository, ICommentService commentService)
    {
        _tvShowRepository = tvShowRepository;
        _commentService = commentService;
    }

    public async Task<GetTvShowModel> GetTvShow(long tvShowId, CancellationToken token)
    {
        return await _tvShowRepository.GetTvShowByIdAsync(tvShowId, token);
    }

    public async Task<GetTvShowTitleModel[]> GetTvShowTitles(long[] tvShowIds, CancellationToken token)
    {
        return await _tvShowRepository.GetTvShowTitleByIdAsync(tvShowIds, token);
    }

    public async Task<long[]> AddTvShow(TvShowModel tvShow, CancellationToken token)
    {
        return await _tvShowRepository.AddAsync(new[] { tvShow }, token);
    }

    public async Task<GetCommentModel[]> GetComments(long tvShowId, CancellationToken token)
    {
        return await _commentService.GetComments(tvShowId, PageType.TvShow, token);
    }

    public async Task AddComment(AddCommentModel comment, CancellationToken token)
    {
        var newComment = new CommentModel()
        {
            Content = comment.Content,
            CreatedByUserId = comment.CreatedByUserId,
            ParentCommentId = comment.ParentCommentId,
            PageId = comment.PageId,
            PageType = (int)PageType.TvShow,
        };
        await _commentService.AddComment(newComment, token);
    }

    public async Task<GetTvShowTitleModel[]> SearchTvShows(string query, CancellationToken token)
    {
        return await _tvShowRepository.SearchTvShowsAsync(query, token);
    }
}