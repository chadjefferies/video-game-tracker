using Example.VideoGameTracker.Api.DataAccess;
using Example.VideoGameTracker.Api.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Example.VideoGameTracker.Api.Controllers
{
    [ApiController]
    [Route("games")]
    public class GamesController : ControllerBase
    {
        private readonly IVideoGameDatabase _videoGameDatabase;

        public GamesController(IVideoGameDatabase videoGameDatabase)
        {
            _videoGameDatabase = videoGameDatabase;
        }

        /// <summary>
        /// Retrieve a list of video games matching the specified filter parameters.
        /// </summary>
        /// <returns>A filtered list of video games.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetGamesAsync([FromQuery] GamesRequest parameters, CancellationToken cancellationToken)
        {
            var gameData = await _videoGameDatabase.GetGamesAsync(parameters.q, parameters.sort, cancellationToken);
            return Ok(gameData);
        }
    }
}