using Example.VideoGameTracker.Api.DataAccess;
using Example.VideoGameTracker.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Example.VideoGameTracker.Api.Controllers
{
    [ApiController]
    [Route("games")]
    public class GamesController : ControllerBase
    {
        private readonly ILogger<GamesController> _logger;
        private readonly IVideoGameDatabase _videoGameDatabase;

        public GamesController(ILogger<GamesController> logger, IVideoGameDatabase videoGameDatabase)
        {
            _logger = logger;
            _videoGameDatabase = videoGameDatabase;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IAsyncEnumerable<Game> GetGamesAsync(string q, string sort, CancellationToken cancellation)
        {
            return _videoGameDatabase.GetGamesAsync(q, sort, cancellation);
        }
    }
}