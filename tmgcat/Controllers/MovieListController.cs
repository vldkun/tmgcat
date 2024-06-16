using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using tmgcat.App.Contracts;
using tmgcat.Bll.Interfaces.Movies;
using tmgcat.Bll.Models.Movies;

namespace tmgcat.App.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieListController : ControllerBase
{
    private readonly IMovieListService _movieListService;

    public MovieListController(IMovieListService movieListService)
    {
        _movieListService = movieListService;
    }

    [Produces(MediaTypeNames.Application.Json)]
    [HttpGet]
    public async Task<ActionResult<MovieListItemDto[]>> Get(long userId)
    {
        var list = await _movieListService.GetList(userId, CancellationToken.None);
        var results = list.Select(m => new MovieListItemDto
        {
            MovieId = m.MovieId,
            TitleEn = m.TitleEn,
            TitleRu = m.TitleEn,
            PosterPath = m.PosterPath,
            Status = m.Status switch
            {
                0 => "Planned",
                1 => "Watched",
                _ => "Not planned"
            },
            UserRating = m.UserRating
        });
        return Ok(results);
    }

    [HttpPost]
    public async Task<ActionResult> Add(long userId, long movieId, string status)
    {
        try
        {
            var movie = new AddMovieListItemModel[]
            {
                new()
                {
                    UserId = userId,
                    MovieId = movieId,
                    Status = status switch
                    {
                        "Planned" => 0,
                        "Watched" => 1,
                        "Not planned" => 2,
                        _ => throw new InvalidOperationException()
                    }
                }
            };

            await _movieListService.AddToList(movie, CancellationToken.None);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest();
        }

        return Ok();
    }
}