using Example.VideoGameTracker.Api.Models;
using static Example.VideoGameTracker.Api.Tests.GameHelpers;

namespace Example.VideoGameTracker.Api.Tests
{
    public class GameTests
    {
        [Fact]
        public void NotEqual()
        {
            var game1 = CreateGame(0);
            var game2 = CreateGame(1);
            Assert.NotEqual(game1, game2);
        }

        [Fact]
        public void Equal()
        {
            var game1 = CreateGame(0);
            var game2 = CreateGame(0);
            Assert.Equal(game1, game2);
        }
    }
}