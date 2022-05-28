using Example.VideoGameTracker.Api.Models;

namespace Example.VideoGameTracker.Api.DataAccess
{
    public interface IVideoGameDatabase
    {
        Task<IEnumerable<Game>> GetGamesAsync(string search, string ordering, CancellationToken cancellation);
        Task<Game> GetGameAsync(int gameId, CancellationToken cancellationToken);
    }
}
