using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using tmgcat.App.Contracts;
using tmgcat.Bll.Interfaces.Games;

namespace tmgcat.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly IGameService _gameService;

        public GameController(
            IGameService gameService,
            ILogger<GameController> logger)
        {
            _logger = logger;
            _gameService = gameService;
        }

        [Produces(MediaTypeNames.Application.Json)]
        [HttpGet]
        public async Task<ActionResult<GameDto>> Get(long id)
        {
            var game = await _gameService.GetGame(id, CancellationToken.None);
            var result = new GameDto
            {
                GameId = game.Id,
                Title = game.Title,
                Description = game.Description,
                UserScore = "None",
                IgdbId = game.IgdbId,
                ReleasedAt = game.ReleasedAt,
                Platforms = game.Platforms,
                CoverPath = game.CoverPath,
                Status = game.Status switch
                {
                    0 => "Released",
                    1 => "Cancelled",
                    2 => "Not yet released",
                    _ => "None",
                },
                Genres = game.Genres,
                Category = game.Category,
                InvolvedCompanies = game.InvolvedCompanies
            };
            return Ok(result);
        }
    }
}
