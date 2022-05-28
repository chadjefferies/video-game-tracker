namespace Example.VideoGameTracker.Api.Models
{
    public class GamesResponse
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public IEnumerable<Game> Results { get; set; }
    }
}
