using tmgcat.Bll.Enums;
using tmgcat.Bll.Interfaces.Comments;
using tmgcat.Bll.Interfaces.Movies;
using tmgcat.Bll.Models.Comments;
using tmgcat.Bll.Models.Movies;

namespace tmgcat.Bll.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly ICommentService _commentService;

    public MovieService(IMovieRepository movieRepository, ICommentService commentService)
    {
        _movieRepository = movieRepository;
        _commentService = commentService;
    }

    public async Task<GetMovieModel> GetMovie(long movieId, CancellationToken token)
    {
        return await _movieRepository.GetMovieByIdAsync(movieId, token);
    }

    public async Task<GetMovieTitleModel[]> GetMovieTitles(long[] movieIds, CancellationToken token)
    {
        return await _movieRepository.GetMovieTitleByIdAsync(movieIds, token);
    }

    public async Task<long[]> AddMovie(MovieModel movie, CancellationToken token)
    {
        return await _movieRepository.AddAsync(new[] { movie }, token);
    }

    public async Task<GetCommentModel[]> GetComments(long movieId, CancellationToken token)
    {
        return await _commentService.GetComments(movieId, PageType.Movie, token);
    }

    public async Task AddComment(AddCommentModel comment, CancellationToken token)
    {
        var newComment = new CommentModel()
        {
            Content = comment.Content,
            CreatedByUserId = comment.CreatedByUserId,
            ParentCommentId = comment.ParentCommentId,
            PageId = comment.PageId,
            PageType = (int)PageType.Movie,
        };
        await _commentService.AddComment(newComment, token);
    }

    public async Task<GetMovieTitleModel[]> SearchMovies(string query, CancellationToken token)
    {
        return await _movieRepository.SearchMoviesAsync(query, token);
    }
}