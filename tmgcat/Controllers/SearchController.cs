using System.Net.Mime;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using tmgcat.App.Contracts;
using tmgcat.Bll.Interfaces.Games;
using tmgcat.Bll.Interfaces.Movies;
using tmgcat.Bll.Interfaces.TVShows;

namespace tmgcat.App.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : ControllerBase
{
    private readonly IGameService _gameService;
    private readonly ITvShowService _tvShowService;
    private readonly IMovieService _movieService;

    public SearchController(
        IGameService gameService,
        ITvShowService tvShowService,
        IMovieService movieService
        )
    {
        _gameService = gameService;
        _tvShowService = tvShowService;
        _movieService = movieService;
    }

    [Route("/Search/Games")]
    [HttpGet]
    public async Task<ActionResult<GameTitleDto[]>> SearchGames(string query)
    {
        var games = await _gameService.SearchGames(query, CancellationToken.None);
        var result = games.Select(g => new GameTitleDto()
        {
            Id = g.Id,
            PosterPath = g.CoverPath,
            Title = g.Title,
            ReleasedAt = g.ReleasedAt
        });
        return Ok(result);
    }
    [Route("/Search/Movies")]
    [HttpGet]
    public async Task<ActionResult<MovieTitleDto[]>> SearchMovies(string query)
    {
        var games = await _movieService.SearchMovies(query, CancellationToken.None);
        var result = games.Select(g => new MovieTitleDto()
        {
            Id = g.Id,
            PosterPath = g.PosterPath,
            TitleEn = g.TitleEn,
            TitleRu = g.TitleRu,
            ReleasedAt = g.ReleasedAt
        });
        return Ok(result);
    }

    [Route("/Search/TvShows")]
    [HttpGet]
    public async Task<ActionResult<TvShowTitleDto[]>> SearchTvShows(string query)
    {
        var games = await _tvShowService.SearchTvShows(query, CancellationToken.None);
        var result = games.Select(g => new TvShowTitleDto()
        {
            Id = g.Id,
            PosterPath = g.PosterPath,
            TitleEn = g.TitleEn,
            TitleRu = g.TitleRu,
            FirstAirDate = g.FirstAirDate,
            LastAirDate = g.LastAirDate,
        });
        return Ok(result);
    }
}