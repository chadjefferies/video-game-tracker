using Example.VideoGameTracker.Api.DataAccess;
using Example.VideoGameTracker.Api.Models;
using Example.VideoGameTracker.Api.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Example.VideoGameTracker.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserDatabase _userDatabase;
        private readonly IVideoGameDatabase _videoGameDatabase;

        public UsersController(ILogger<UsersController> logger, IUserDatabase userDatabase, IVideoGameDatabase videoGameDatabase)
        {
            _logger = logger;
            _userDatabase = userDatabase;
            _videoGameDatabase = videoGameDatabase;
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateNewUser(UserRequest user, CancellationToken cancellationToken)
        {
            var newUser = await _userDatabase.AddNewAsync(user, cancellationToken);
            if (newUser is null)
            {
                return Conflict();
            }

            _logger.LogDebug("Created new user {userId}", newUser.UserId);
            return Created(string.Empty, newUser);
        }

        /// <summary>
        /// Get a user by the specified userId
        /// </summary>
        [HttpGet]
        [Route("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(int userId, CancellationToken cancellationToken)
        {
            var user = await _userDatabase.GetAsync(userId, cancellationToken);
            if (user is null)
            {
                return NotFound($"User {userId} does not exist.");
            }

            return Ok(user);
        }

        /// <summary>
        /// Add a game to the user's list of favorite games.
        /// </summary>
        [HttpPost]
        [Route("{userId}/games")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddFavoriteGame(int userId, [FromBody] AddFavoriteRequest request, CancellationToken cancellationToken)
        {
            var game = await _videoGameDatabase.GetGameAsync(request.GameId, cancellationToken);
            if (game is null)
            {
                return BadRequest($"Game {request.GameId} is not a valid game.");
            }

            var user = await _userDatabase.GetAsync(userId, cancellationToken);
            if (user is null)
            {
                return NotFound($"User {userId} does not exist.");
            }

            if (!user.Games.AddFavorite(game))
            {
                return Conflict($"Game {game.Id} is already a part of this user's favorites.");
            }

            await _userDatabase.UpdateAsync(user, cancellationToken);

            _logger.LogDebug("Added game {gameId} to user {userId} favorites", game.Id, userId);

            return NoContent();
        }

        /// <summary>
        /// Remove a game from the user's list of favorite games.
        /// </summary>
        [HttpDelete]
        [Route("{userId}/games/{gameId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveFavoriteGame(int userId, int gameId, CancellationToken cancellationToken)
        {
            var user = await _userDatabase.GetAsync(userId, cancellationToken);
            if (user is null)
            {
                return NotFound($"User {userId} does not exist.");
            }

            if (!user.Games.RemoveFavorite(new Game(gameId)))
            {
                return NotFound($"Game {gameId} is not in this user's list of favorites.");
            }

            await _userDatabase.UpdateAsync(user, cancellationToken);

            _logger.LogDebug("Removed game {gameId} from user {userId} favorites", gameId, userId);

            return NoContent();
        }

        /// <summary>
        /// Compare the user's list of favorite games to another.
        /// </summary>
        [HttpPost]
        [Route("{userId}/comparison")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CompareUserFavorites(int userId, [FromBody] ComparisonRequest comparisonRequest, CancellationToken cancellationToken)
        {
            var user = await _userDatabase.GetAsync(userId, cancellationToken);
            if (user is null)
            {
                return NotFound($"User {userId} does not exist.");
            }

            var otherUser = await _userDatabase.GetAsync(comparisonRequest.OtherUserId, cancellationToken);
            if (otherUser is null)
            {
                return BadRequest($"User {comparisonRequest.OtherUserId} does not exist.");
            }

            var gameComparison = user.Games.CompareFavorites(otherUser.Games, comparisonRequest.Comparison);

            var result = new ComparisonResult(
                user.UserId, 
                otherUser.UserId, 
                comparisonRequest.Comparison, 
                gameComparison);

            return Ok(result);
        }
    }
}