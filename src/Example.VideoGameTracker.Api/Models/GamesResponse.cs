namespace Example.VideoGameTracker.Api.Models
{
    public class GamesResponse
    {
        public int Count { get; }
        public string? Next { get; }
        public IEnumerable<Game>? Results { get; }

        public GamesResponse(int count, string? next, IEnumerable<Game>? results)
        {
            Count = count;
            Next = next;
            Results = results;
        }
    }
}
