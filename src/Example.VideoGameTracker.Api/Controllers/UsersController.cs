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

        public UsersController(ILogger<UsersController> logger, InMemoryUserDatabase userDatabase)
        {
            _logger = logger;
            _userDatabase = userDatabase;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult CreateNewUser(User user)
        {
            if (_userDatabase.AddNewUser(user))
            {
                return StatusCode(StatusCodes.Status201Created, true);
            }

            return StatusCode(StatusCodes.Status409Conflict);
        }

        [HttpGet]
        [Route("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public User GetUser(int userId)
        {
            return _userDatabase.GetUser(userId);
        }

        [HttpPost]
        [Route("{userId}/games")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult AddFavoriteGame(int userId)
        {
            return StatusCode(StatusCodes.Status201Created, true);
        }

        [HttpDelete]
        [Route("{userId}/games/{gameId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RemoveFavoriteGame(int userId, int gameId)
        {
            var user = _userDatabase.GetUser(userId);
            user.FavoriteGames.Compare(null, FavoriteGameComparison.Union);
            return StatusCode(StatusCodes.Status201Created, true);
        }

        [HttpPost]
        [Route("{userId}/union")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UnionUserFavorites(int userId)
        {
            return StatusCode(StatusCodes.Status201Created, true);

        }

        [HttpPost]
        [Route("{userId}/intersection")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult IntersectUserFavorites(int userId)
        {
            return StatusCode(StatusCodes.Status201Created, true);

        }

        [HttpPost]
        [Route("{userId}/difference")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DiffUserFavorites(int userId)
        {
            return StatusCode(StatusCodes.Status201Created, true);

        }
    }
}