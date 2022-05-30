using System.Collections;

namespace Example.VideoGameTracker.Api.Models
{
    public class FavoriteGames : IEnumerable<Game>
    {
        private readonly HashSet<Game> _games;

        public IReadOnlySet<Game> Games => _games;

        public FavoriteGames(IEnumerable<Game> games)
        {
            _games = new HashSet<Game>(games);
        }

        public bool AddFavorite(Game game)
        {
            return _games.Add(game);
        }

        public bool RemoveFavorite(Game game)
        {
            return _games.Remove(game);
        }

        public IEnumerable<Game> CompareFavorites(FavoriteGames other, FavoriteGameComparison mode)
        {
            return mode switch
            {
                FavoriteGameComparison.Union => _games.Union(other.Games),
                FavoriteGameComparison.Intersection => _games.Intersect(other.Games),
                FavoriteGameComparison.Difference => other._games.Except(Games),
                _ => throw new NotImplementedException(),
            };
        }

        public IEnumerator<Game> GetEnumerator() => _games.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _games.GetEnumerator();
    }
}
