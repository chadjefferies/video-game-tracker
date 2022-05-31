namespace Example.VideoGameTracker.Api.Models
{
    public class ComparisonRequest
    {
        public int OtherUserId { get; set; }
        public GameComparisonType Comparison { get; set; }
    }
}
