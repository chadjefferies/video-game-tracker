using System.Collections;

namespace Example.VideoGameTracker.Api.Models
{
    public class FavoriteGameCollection : IEnumerable<Game>
    {
        private readonly HashSet<Game> _games;

        public IReadOnlySet<Game> Games => _games;

        public FavoriteGameCollection(IEnumerable<Game> games)
        {
            _games = new HashSet<Game>(games);
        }

        public FavoriteGameCollection()
            : this(Enumerable.Empty<Game>())
        { }

        public bool AddFavorite(Game game) => _games.Add(game);

        public bool RemoveFavorite(int gameId) => _games.Remove(new Game(gameId));

        public IEnumerable<Game> CompareFavorites(FavoriteGameCollection other, GameComparisonType mode)
        {
            return mode switch
            {
                GameComparisonType.Union => _games.Union(other.Games),
                GameComparisonType.Intersection => _games.Intersect(other.Games),
                GameComparisonType.Difference => other._games.Except(Games),
                _ => throw new NotImplementedException(),
            };
        }

        public IEnumerator<Game> GetEnumerator() => _games.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _games.GetEnumerator();
    }
}
