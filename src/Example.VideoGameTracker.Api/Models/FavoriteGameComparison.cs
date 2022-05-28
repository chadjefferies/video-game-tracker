namespace Example.VideoGameTracker.Api.Models
{
    public enum FavoriteGameComparison
    {
        /// <summary>
        /// list the favorite games of both users.
        /// </summary>
        Union,
        /// <summary>
        /// list the favorite games that both users have in common.
        /// </summary>
        Intersection,
        /// <summary>
        /// list the favorite games added by the other user but not by the current user.
        /// </summary>
        Difference
    }
}
