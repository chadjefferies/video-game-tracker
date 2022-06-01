namespace Example.VideoGameTracker.Api.Models
{
    public class ComparisonResult
    {
        public int UserId { get; }
        public int OtherUserId { get; }
        public GameComparisonType Comparison { get; }
        public IEnumerable<Game>? Games { get; }

        public ComparisonResult(int userId, int otherUserId, GameComparisonType comparison, IEnumerable<Game>? games)
        {
            UserId = userId;
            OtherUserId = otherUserId;
            Comparison = comparison;
            Games = games;
        }
    }
}
