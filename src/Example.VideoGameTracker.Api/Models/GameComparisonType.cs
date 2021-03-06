namespace Example.VideoGameTracker.Api.Models
{
    public enum GameComparisonType
    {
        /// <summary>
        /// List the favorite games of both users.
        /// </summary>
        Union,
        /// <summary>
        /// List the favorite games that both users have in common.
        /// </summary>
        Intersection,
        /// <summary>
        /// List the favorite games added by the other user but not by the current user.
        /// </summary>
        Difference
    }
}
