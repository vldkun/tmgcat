using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using tmgcat.App.Contracts;
using tmgcat.Bll.Interfaces.TVShows;
using tmgcat.Bll.Models.Comments;

namespace tmgcat.App.Controllers;

[ApiController]
[Route("[controller]")]
public class TvShowController : ControllerBase
{
    private readonly ITvShowService _tvShowService;
    private readonly ITvShowListService _tvShowListService;

    public TvShowController(ITvShowService tvShowService, ITvShowListService tvShowListService)
    {
        _tvShowService = tvShowService;
        _tvShowListService = tvShowListService;
    }

    [Produces(MediaTypeNames.Application.Json)]
    [HttpGet]
    public async Task<ActionResult<TvShowDto>> Get(long id)
    {
        var tvShow = await _tvShowService.GetTvShow(id, CancellationToken.None);
        var result = new TvShowDto
        {
            TvShowId = tvShow.Id,
            TitleEn = tvShow.TitleEn,
            TitleRu = tvShow.TitleRu,
            Description = tvShow.Description,
            UserRating = tvShow.UserRating is not null ? $"{tvShow.UserRating:F1}" : null,
            ImdbId = tvShow.ImdbId,
            TmdbId = tvShow.TmdbId,
            ImdbRating = tvShow.ImdbRating is not null ? $"{tvShow.ImdbRating:F1}" : null,
            KpRating = tvShow.KpRating is not null ? $"{tvShow.KpRating:F1}" : null,
            AvgEpRuntime = tvShow.AvgEpRuntime,
            FirstAirDate = tvShow.FirstAirDate,
            LastAirDate = tvShow.LastAirDate,
            PosterPath = tvShow.PosterPath,
            Status = tvShow.Status switch
            {
                0 => "Закончен",
                1 => "В эфире",
                2 => "На паузе",
                3 => "Скоро выйдет",
                _ => "None",
            },
            Genres = tvShow.Genres,
            Creators = tvShow.Creators,
            EpisodesNumber = tvShow.EpisodesNumber,
            SeasonsNumber = tvShow.SeasonsNumber
        };
        return Ok(result);
    }

    [Produces(MediaTypeNames.Application.Json)]
    [HttpGet]
    [Route("{tvShowId}/Status/{userId}")]
    public async Task<ActionResult<string>> GetUserStatus(long tvShowId, long userId)
    {
        var status = await _tvShowListService.GetUserStatus(userId, tvShowId, CancellationToken.None);
        var result = status switch
        {
            0 => "Planned",
            1 => "Watched",
            2 => "Watching",
            _ => "Not planned"
        };
        return Ok(result);
    }


    [HttpPost]
    [Route("{tvShowId}/Status/{userId}")]
    public async Task<ActionResult> ChangeUserStatus(long tvShowId, long userId, string status)
    {
        try
        {
            var intStatus = status switch
            {
                "Planned" => 0,
                "Watched" => 1,
                "Watching" => 2,
                "Not planned" => 3,
                _ => throw new InvalidOperationException()
            };
            await _tvShowListService.ChangeUserStatus(userId, tvShowId, intStatus, CancellationToken.None);
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpGet]
    [Route("{tvShowId}/Comments")]
    public async Task<ActionResult<CommentDto[]>> GetComments(long tvShowId)
    {
        var comments = await _tvShowService.GetComments(tvShowId, CancellationToken.None);
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
    [Route("{tvShowId}/Comments")]
    public async Task<ActionResult> AddComment(long tvShowId, string content, long createdByUserId,
        long? parentCommentId)
    {
        var comment = new AddCommentModel()
        {
            Content = content,
            CreatedByUserId = createdByUserId,
            ParentCommentId = parentCommentId,
            PageId = tvShowId
        };
        await _tvShowService.AddComment(comment, CancellationToken.None);
        return Ok();
    }

    [HttpPost]
    [Route("{tvShowId}/Score/{userId}")]
    public async Task<ActionResult> ChangeUserScore(long tvShowId, long userId, int score)
    {
        try
        {
            await _tvShowListService.ChangeUserRating(userId, tvShowId, score, CancellationToken.None);
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpPost]
    [Route("{tvShowId}/Episodes/{userId}")]
    public async Task<ActionResult> ChangePlayingTime(long tvShowId, long userId, int episodes)
    {
        try
        {
            await _tvShowListService.ChangeEpisodesNumber(userId, tvShowId, episodes, CancellationToken.None);
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok();
    }
}