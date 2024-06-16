using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using tmgcat.App.Contracts;
using tmgcat.Bll.Interfaces.Movies;
using tmgcat.Bll.Models.Comments;

namespace tmgcat.App.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;
    private readonly IMovieListService _movieListService;

    public MovieController(IMovieService movieService, IMovieListService movieListService)
    {
        _movieService = movieService;
        _movieListService = movieListService;
    }

    [Produces(MediaTypeNames.Application.Json)]
    [HttpGet]
    public async Task<ActionResult<MovieDto>> Get(long id)
    {
        var movie = await _movieService.GetMovie(id, CancellationToken.None);
        var result = new MovieDto
        {
            MovieId = movie.Id,
            TitleEn = movie.TitleEn,
            TitleRu = movie.TitleRu,
            Description = movie.Description,
            UserRating = movie.UserRating is not null ? $"{movie.UserRating:F1}" : null,
            ImdbId = movie.ImdbId,
            TmdbId = movie.TmdbId,
            ImdbRating = movie.ImdbRating is not null ? $"{movie.ImdbRating:F1}" : null,
            KpRating = movie.KpRating is not null ? $"{movie.KpRating:F1}" : null,
            ReleasedAt = movie.ReleasedAt,
            PosterPath = movie.PosterPath,
            Status = movie.Status switch
            {
                0 => "Вышел",
                1 => "Ожидается дата релиза",
                2 => "Скоро выйдет",
                _ => "None",
            },
            Genres = movie.Genres,
            Creators = movie.Creators,
            RuntimeMinutes = movie.RuntimeMinutes
        };
        return Ok(result);
    }

    [Produces(MediaTypeNames.Application.Json)]
    [HttpGet]
    [Route("{movieId}/Status/{userId}")]
    public async Task<ActionResult<string>> GetUserStatus(long movieId, long userId)
    {
        var status = await _movieListService.GetUserStatus(userId, movieId, CancellationToken.None);
        var result = status switch
        {
            0 => "Planned",
            1 => "Watched",
            _ => "Not planned"
        };
        return Ok(result);
    }


    [HttpPost]
    [Route("{movieId}/Status/{userId}")]
    public async Task<ActionResult> ChangeUserStatus(long movieId, long userId, string status)
    {
        try
        {
            var intStatus = status switch
            {
                "Planned" => 0,
                "Watched" => 1,
                "Not planned" => 2,
                _ => throw new InvalidOperationException()
            };
            await _movieListService.ChangeUserStatus(userId, movieId, intStatus, CancellationToken.None);
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpGet]
    [Route("{movieId}/Comments")]
    public async Task<ActionResult<CommentDto[]>> GetComments(long movieId)
    {
        var comments = await _movieService.GetComments(movieId, CancellationToken.None);
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
    [Route("{movieId}/Comments")]
    public async Task<ActionResult> AddComment(long movieId, string content, long createdByUserId,
        long? parentCommentId)
    {
        var comment = new AddCommentModel()
        {
            Content = content,
            CreatedByUserId = createdByUserId,
            ParentCommentId = parentCommentId,
            PageId = movieId
        };
        await _movieService.AddComment(comment, CancellationToken.None);
        return Ok();
    }

    [HttpPost]
    [Route("{movieId}/Score/{userId}")]
    public async Task<ActionResult> ChangeUserScore(long movieId, long userId, int score)
    {
        try
        {
            await _movieListService.ChangeUserRating(userId, movieId, score, CancellationToken.None);
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok();
    }
}