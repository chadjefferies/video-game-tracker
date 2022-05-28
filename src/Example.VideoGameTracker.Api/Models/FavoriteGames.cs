namespace Example.VideoGameTracker.Api.Models
{
    public class FavoriteGames
    {
        public IReadOnlyList<Game> Games { get; }

        public FavoriteGames(IEnumerable<Game> games)
        {
            Games = games.ToList();
        }

        public IEnumerable<Game> Compare(FavoriteGames other, FavoriteGameComparison mode)
        {
            return mode switch
            {
                FavoriteGameComparison.Union => Games.Union(other.Games),
                FavoriteGameComparison.Intersection => Games.Intersect(other.Games),
                FavoriteGameComparison.Difference => other.Games.Except(Games),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
