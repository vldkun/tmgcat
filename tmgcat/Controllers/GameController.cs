using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using tmgcat.App.Contracts;
using tmgcat.Bll.Interfaces.Games;
using tmgcat.Bll.Models.Comments;

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
                UserRating = game.UserRating is not null ? $"{game.UserRating:F1}" : null,
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
        public async Task<ActionResult> ChangeUserStatus(long gameId, long userId, string status)
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

        [HttpPost]
        [Route("{gameId}/Played/{userId}")]
        public async Task<ActionResult> ChangePlayingTime(long gameId, long userId, int time)
        {
            try
            {
                await _gameListService.ChangePlayingTime(userId, gameId, time, CancellationToken.None);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost]
        [Route("{gameId}/Score/{userId}")]
        public async Task<ActionResult> ChangeUserScore(long gameId, long userId, int score)
        {
            try
            {
                await _gameListService.ChangeUserRating(userId, gameId, score, CancellationToken.None);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        [Route("{gameId}/Comments")]
        public async Task<ActionResult<CommentDto[]>> GetComments(long gameId)
        {
            var comments = await _gameService.GetComments(gameId, CancellationToken.None);
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
        [Route("{gameId}/Comments")]
        public async Task<ActionResult> AddComment(long gameId, string content, long createdByUserId,
            long? parentCommentId)
        {
            var comment = new AddCommentModel()
            {
                Content = content,
                CreatedByUserId = createdByUserId,
                ParentCommentId = parentCommentId,
                PageId = gameId
            };
            await _gameService.AddComment(comment, CancellationToken.None);
            return Ok();
        }
    }
}
