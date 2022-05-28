﻿namespace Example.VideoGameTracker.Api.Models
{
    public class ComparisonResponse
    {
        public int UserId { get; set; }
        public int OtherUserId { get; set; }
        public FavoriteGameComparison Comparison { get; set; }
        public IReadOnlyList<Game> Games { get; set; }
    }
}
