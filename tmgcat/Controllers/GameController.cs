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
        private readonly IGameListService _gameListService;

        public GameController(
            IGameListService gameListService,
            IGameService gameService,
            ILogger<GameController> logger)
        {
            _logger = logger;
            _gameService = gameService;
            _gameListService = gameListService;
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

        [Produces(MediaTypeNames.Application.Json)]
        [HttpGet]
        [Route("{gameId}/Status/{userId}")]
        public async Task<ActionResult<string>> GetUserStatus(long gameId, long userId)
        {
            var status = await _gameListService.GetUserStatus(userId, gameId, CancellationToken.None);
            var result = status switch
            {
                0 => "Planned",
                1 => "Playing",
                2 => "Played",
                _ => "Not planned"
            };
            return Ok(result);
        }

        
        [HttpPost]
        [Route("{gameId}/Status/{userId}")]
        public async Task<ActionResult<string>> ChangeUserStatus(long gameId, long userId, string status)
        {
            try
            {
                var intStatus = status switch
                {
                    "Planned" => 0,
                    "Playing" => 1,
                    "Played" => 2,
                    "Not planned" => 3,
                    _ => throw new InvalidOperationException()
                };
                await _gameListService.ChangeUserStatus(userId, gameId, intStatus, CancellationToken.None);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            
            return Ok();
        }
    }
}
