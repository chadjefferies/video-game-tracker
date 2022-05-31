namespace Example.VideoGameTracker.Api.Models.Request
{
    public class ComparisonRequest
    {
        public int OtherUserId { get; set; }
        public GameComparisonType Comparison { get; set; }
    }
}
