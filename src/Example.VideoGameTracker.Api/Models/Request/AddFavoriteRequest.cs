namespace Example.VideoGameTracker.Api.Models.Request
{
    public class AddFavoriteRequest
    {
        public int GameId { get; }
        public AddFavoriteRequest(int gameId)        {
            GameId = gameId;
        }

    }
}
