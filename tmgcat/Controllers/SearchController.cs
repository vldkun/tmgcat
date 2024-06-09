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
    //private readonly ITvShowService _tvShowService;
    //private readonly IMovieService _movieService;

    public SearchController(
        IGameService gameService
        //ITvShowService tvShowService,
        //IMovieService movieService
        )
    {
        _gameService = gameService;
        //_tvShowService = tvShowService;
       // _movieService = movieService;
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
}