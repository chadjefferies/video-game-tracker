namespace Example.VideoGameTracker.Api.Models
{
    public class ComparisonResult
    {
        public int UserId { get; set; }
        public int OtherUserId { get; set; }
        public GameComparisonType Comparison { get; set; }
        public IEnumerable<Game>? Games { get; set; }
    }
}
