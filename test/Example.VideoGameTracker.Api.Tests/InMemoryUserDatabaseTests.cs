using Example.VideoGameTracker.Api.DataAccess;
using Example.VideoGameTracker.Api.Models;
using static Example.VideoGameTracker.Api.Tests.GameHelpers;

namespace Example.VideoGameTracker.Api.Tests
{
    public class InMemoryUserDatabaseTests
    {
        [Fact]
        public async Task AddAndGet()
        {
            var db = new InMemoryUserDatabase();
            var addUser = new UserRequest("johnny", "gamer");
            var result = await db.AddNewAsync(addUser);
            Assert.NotNull(result);
            var getUser = await db.GetAsync(result.UserId);
            Assert.NotNull(getUser);
            Assert.Equal("johnny", getUser.FirstName);
            Assert.Equal("gamer", getUser.LastName);
            Assert.Empty(getUser.Games);
        }

        [Fact]
        public async Task AddUpdateAndGet()
        {
            var db = new InMemoryUserDatabase();
            var addUser = new UserRequest("jenny", "gamer");
            var result = await db.AddNewAsync(addUser);
            Assert.NotNull(result);
            var getUser = await db.GetAsync(result.UserId);
            Assert.NotNull(getUser);
            Assert.Equal("jenny", getUser.FirstName);
            Assert.Equal("gamer", getUser.LastName);
            Assert.Empty(getUser.Games);

            // since the in-memory db is storing reference types
            // this update test isn't all that relevant.
            getUser.Games.AddFavorite(CreateGame(0));
            var updateResult = await db.UpdateAsync(getUser);
            Assert.True(updateResult);
            var getUser2 = await db.GetAsync(getUser.UserId);
            Assert.NotNull(getUser2);
            Assert.Equal("jenny", getUser2.FirstName);
            Assert.Equal("gamer", getUser2.LastName);
            Assert.Single(getUser2.Games);
            Assert.Collection(getUser2.Games,
                g => AssertGame(0, g)
            );
        }
    }
}