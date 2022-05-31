using Example.VideoGameTracker.Api.Models;

namespace Example.VideoGameTracker.Api.Tests
{
    public static class GameHelpers
    {
        public static void AssertGame(int i, Game g)
        {
            Assert.Equal(i, g.Id);
            Assert.Equal($"game{i}", g.Name);
            Assert.Equal(i, g.Added);
            Assert.Equal(i, g.Metacritic);
            Assert.Equal(i, g.Rating);
        }

        public static Game CreateGame(int i)
        {
            return new Game
            {
                Id = i,
                Name = $"game{i}",
                Added = i,
                Metacritic = i,
                Rating = i
            };
        }
    }
}