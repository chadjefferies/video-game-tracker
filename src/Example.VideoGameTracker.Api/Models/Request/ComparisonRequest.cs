namespace Example.VideoGameTracker.Api.Models.Request
{
    public class ComparisonRequest
    {
        public int OtherUserId { get; }
        public GameComparisonType Comparison { get; }

        public ComparisonRequest(int otherUserId, GameComparisonType comparison)
        {
            OtherUserId = otherUserId;
            Comparison = comparison;
        }
    }
}
