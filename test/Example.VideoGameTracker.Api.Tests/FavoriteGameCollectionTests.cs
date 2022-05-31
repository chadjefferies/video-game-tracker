using Example.VideoGameTracker.Api.Models;
using static Example.VideoGameTracker.Api.Tests.GameHelpers;

namespace Example.VideoGameTracker.Api.Tests
{
    public class FavoriteGameCollectionTests
    {
        [Fact]
        public void Compare_Union()
        {
            var games1 = Enumerable.Range(0, 2).Select(x => CreateGame(x));
            var games2 = Enumerable.Range(0, 3).Select(x => CreateGame(x));
            var faves1 = new FavoriteGameCollection(games1);
            var faves2 = new FavoriteGameCollection(games2);
            var results = faves1.CompareFavorites(faves2, GameComparisonType.Union).ToList();
            Assert.Equal(3, results.Count);
            Assert.Collection(results,
                g => AssertGame(0, g),
                g => AssertGame(1, g),
                g => AssertGame(2, g)
            );
        }

        [Fact]
        public void Compare_Union_Empty()
        {
            var games2 = Enumerable.Range(0, 3).Select(x => CreateGame(x));
            var faves1 = new FavoriteGameCollection();
            var faves2 = new FavoriteGameCollection(games2);
            var results = faves1.CompareFavorites(faves2, GameComparisonType.Union).ToList();
            Assert.Equal(3, results.Count);
            Assert.Collection(results,
                g => AssertGame(0, g),
                g => AssertGame(1, g),
                g => AssertGame(2, g)
            );
        }

        [Fact]
        public void Compare_Union_OtherEmpty()
        {
            var games1 = Enumerable.Range(0, 2).Select(x => CreateGame(x));
            var faves1 = new FavoriteGameCollection(games1);
            var faves2 = new FavoriteGameCollection();
            var results = faves1.CompareFavorites(faves2, GameComparisonType.Union).ToList();
            Assert.Equal(2, results.Count);
            Assert.Collection(results,
                g => AssertGame(0, g),
                g => AssertGame(1, g)
            );
        }

        [Fact]
        public void Compare_Intersection()
        {
            var games1 = Enumerable.Range(0, 2).Select(x => CreateGame(x));
            var games2 = Enumerable.Range(0, 3).Select(x => CreateGame(x));
            var faves1 = new FavoriteGameCollection(games1);
            var faves2 = new FavoriteGameCollection(games2);
            var results = faves1.CompareFavorites(faves2, GameComparisonType.Intersection).ToList();
            Assert.Equal(2, results.Count);
            Assert.Collection(results,
                g => AssertGame(0, g),
                g => AssertGame(1, g)
            );
        }

        [Fact]
        public void Compare_Intersection_Empty()
        {
            var games2 = Enumerable.Range(0, 3).Select(x => CreateGame(x));
            var faves1 = new FavoriteGameCollection();
            var faves2 = new FavoriteGameCollection(games2);
            var results = faves1.CompareFavorites(faves2, GameComparisonType.Intersection).ToList();
            Assert.Empty(results);
        }

        [Fact]
        public void Compare_Intersection_OtherEmpty()
        {
            var games1 = Enumerable.Range(0, 2).Select(x => CreateGame(x));
            var faves1 = new FavoriteGameCollection(games1);
            var faves2 = new FavoriteGameCollection();
            var results = faves1.CompareFavorites(faves2, GameComparisonType.Intersection).ToList();
            Assert.Empty(results);
        }

        [Fact]
        public void Compare_Difference()
        {
            var games1 = Enumerable.Range(0, 2).Select(x => CreateGame(x));
            var games2 = Enumerable.Range(0, 3).Select(x => CreateGame(x));
            var faves1 = new FavoriteGameCollection(games1);
            var faves2 = new FavoriteGameCollection(games2);
            var results = faves1.CompareFavorites(faves2, GameComparisonType.Difference).ToList();
            Assert.Single(results);
            Assert.Collection(results,
               g => AssertGame(2, g)
           );
        }

        [Fact]
        public void Compare_Difference_Empty()
        {
            var games2 = Enumerable.Range(0, 3).Select(x => CreateGame(x));
            var faves1 = new FavoriteGameCollection();
            var faves2 = new FavoriteGameCollection(games2);
            var results = faves1.CompareFavorites(faves2, GameComparisonType.Difference).ToList();
            Assert.Equal(3, results.Count);
            Assert.Collection(results,
                g => AssertGame(0, g),
                g => AssertGame(1, g),
                g => AssertGame(2, g)
            );
        }

        [Fact]
        public void Compare_Difference_OtherEmpty()
        {
            var games1 = Enumerable.Range(0, 2).Select(x => CreateGame(x));
            var faves1 = new FavoriteGameCollection(games1);
            var faves2 = new FavoriteGameCollection();
            var results = faves1.CompareFavorites(faves2, GameComparisonType.Difference).ToList();
            Assert.Empty(results);
        }
    }
}