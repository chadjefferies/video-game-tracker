using Example.VideoGameTracker.Api.DataAccess;
using Example.VideoGameTracker.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Example.VideoGameTracker.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly InMemoryUserDatabase _userDatabase;
        private readonly IVideoGameDatabase _videoGameDatabase;

        public UsersController(ILogger<UsersController> logger, InMemoryUserDatabase userDatabase, IVideoGameDatabase videoGameDatabase)
        {
            _logger = logger;
            _userDatabase = userDatabase;
            _videoGameDatabase = videoGameDatabase;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateNewUser(UserRequest user)
        {
            if (await _userDatabase.AddNewUserAsync(new User(user)))
            {
                return StatusCode(StatusCodes.Status201Created);
            }

            return Conflict();
        }

        [HttpGet]
        [Route("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _userDatabase.GetUserAsync(userId);
            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("{userId}/games")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddFavoriteGame(int userId, [FromBody] AddFavoriteRequest request, CancellationToken cancellationToken)
        {
            var user = await _userDatabase.GetUserAsync(userId);
            if (user is null)
            {
                return NotFound();
            }

            var game = await _videoGameDatabase.GetGameAsync(request.GameId, cancellationToken);
            if (game is null)
            {
                return BadRequest();
            }

            // TODO: thread safety
            if (!user.Games.AddFavorite(game))
            {
                return Conflict();
            }

            // TODO: update database? can't depend on a local reference

            return NoContent();
        }

        [HttpDelete]
        [Route("{userId}/games/{gameId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveFavoriteGame(int userId, int gameId)
        {
            var user = await _userDatabase.GetUserAsync(userId);
            if (user is null)
            {
                return NotFound();
            }

            // TODO: thread safety
            if (!user.Games.RemoveFavorite(new Game(gameId)))
            {
                return NotFound($"Game {gameId}");
            }

            // TODO: update database? can't depend on a local reference

            return NoContent();
        }

        [HttpPost]
        [Route("{userId}/comparison")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DiffUserFavorites(
            int userId,
            [FromBody] ComparisonRequest comparisonRequest)
        {
            var user = await _userDatabase.GetUserAsync(userId);
            if (user is null)
            {
                return NotFound();
            }

            var otherUser = await _userDatabase.GetUserAsync(comparisonRequest.OtherUserId);
            if (otherUser is null)
            {
                return BadRequest();
            }

            var gameComparison = user.Games.Compare(otherUser.Games, comparisonRequest.Comparison);

            var response = new ComparisonResponse()
            {
                UserId = user.UserId,
                OtherUserId = otherUser.UserId,
                Comparison = comparisonRequest.Comparison,
                Games = gameComparison.ToList()
            };

            return Ok(response);

        }
    }
}