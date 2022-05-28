namespace Example.VideoGameTracker.Api.Models
{
    public class ComparisonRequest
    {
        public int OtherUserId { get; set; }
        public FavoriteGameComparison Comparison { get; set; }
    }
}
