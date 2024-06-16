using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using tmgcat.App.Contracts;
using tmgcat.Bll.Interfaces.TVShows;
using tmgcat.Bll.Models.TVShows;

namespace tmgcat.App.Controllers;

[ApiController]
[Route("[controller]")]
public class TvShowListController : ControllerBase
{
    private readonly ITvShowListService _tvShowListService;

    public TvShowListController(ITvShowListService tvShowListService)
    {
        _tvShowListService = tvShowListService;
    }

    [Produces(MediaTypeNames.Application.Json)]
    [HttpGet]
    public async Task<ActionResult<TvShowListItemDto[]>> Get(long userId)
    {
        var list = await _tvShowListService.GetList(userId, CancellationToken.None);
        var results = list.Select(t => new TvShowListItemDto
        {
            TvShowId = t.TvShowId,
            TitleEn = t.TitleEn,
            TitleRu = t.TitleEn,
            PosterPath = t.PosterPath,
            Status = t.Status switch
            {
                0 => "Planned",
                1 => "Watched",
                2 => "Watching",
                _ => "Not planned"
            },
            UserRating = t.UserRating,
            EpisodesWatched = t.EpisodesWatched,
        });
        return Ok(results);
    }

    [HttpPost]
    public async Task<ActionResult> Add(long userId, long tvShowId, string status)
    {
        try
        {
            var tvShow = new AddTvShowListItemModel[]
            {
                new()
                {
                    UserId = userId,
                    TvShowId = tvShowId,
                    Status = status switch
                    {
                        "Planned" => 0,
                        "Watched" => 1,
                        "Watching" => 2,
                        "Not planned" => 3,
                        _ => throw new InvalidOperationException()
                    },
                    EpisodesWatched = 0
                }
            };

            await _tvShowListService.AddToList(tvShow, CancellationToken.None);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest();
        }

        return Ok();
    }
}