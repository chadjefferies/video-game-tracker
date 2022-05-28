using Example.VideoGameTracker.Api.Models;

namespace Example.VideoGameTracker.Api.DataAccess
{
    public interface IVideoGameDatabase
    {
        IAsyncEnumerable<Game> GetGamesAsync(string search, string ordering, CancellationToken cancellation);
    }
}
