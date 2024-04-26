using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using tmgcat.App.Contracts;
using tmgcat.Bll.Interfaces;

namespace tmgcat.App.Controllers;

[ApiController]
[Route("[controller]")]
public class GameListController : ControllerBase
{
    private readonly ILogger<GameListController> _logger;
    private readonly IGameListService _gameListService;

    public GameListController(ILogger<GameListController> logger, IGameListService gameListService)
    {
        _logger = logger;
        _gameListService = gameListService;
    }

    [Produces(MediaTypeNames.Application.Json)]
    [HttpGet]
    public async Task<ActionResult<GameListItemDto[]>> Get(long userId)
    {
        var list = await _gameListService.GetList(userId, CancellationToken.None);
        var results = list.Select(g => new GameListItemDto
        {
            GameId = g.GameId,
            Title = g.Title,
            CoverPath = g.CoverPath,
            Status = g.Status switch
            {
                0 => "Planned",
                1 => "Playing",
                2 => "Played",
                3 => "Not planned",
                _ => "None"
            },
            MinutesPlayed = g.MinutesPlayed,
            UserRating = g.UserRating
        });
        return Ok(results);
    }
}