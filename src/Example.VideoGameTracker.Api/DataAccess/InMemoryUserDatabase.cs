using System.Collections.Concurrent;
using Example.VideoGameTracker.Api.Models;
using Example.VideoGameTracker.Api.Models.Request;

/********************************************************
 * Async in this class is overhead, but this is an implementation of IUserDatabase, 
 * which could be implemented for other backend datasources beyond local memory, 
 * where network calls are highly likely
 ********************************************************/

namespace Example.VideoGameTracker.Api.DataAccess
{
    public class InMemoryUserDatabase : IUserDatabase
    {
        private static int _globalIdentityUserId = 0;

        private readonly ConcurrentDictionary<int, User> _users;

        public InMemoryUserDatabase()
        {
            _users = new ConcurrentDictionary<int, User>();
        }

        public ValueTask<User?> AddNewAsync(UserRequest user, CancellationToken cancellationToken)
        {
            int userId = Interlocked.Increment(ref _globalIdentityUserId);
            var newUser = new User(userId, user.FirstName, user.LastName);
            if (_users.TryAdd(newUser.UserId, newUser))
            {
                return ValueTask.FromResult<User?>(newUser);
            }

            return ValueTask.FromResult<User?>(default);
        }

        public ValueTask<User?> GetAsync(int userId, CancellationToken cancellationToken)
        {
            if (_users.TryGetValue(userId, out var user))
            {
                return ValueTask.FromResult<User?>(user);
            }

            return ValueTask.FromResult<User?>(default);
        }

        public ValueTask<bool> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            // since we are doing a blind update here
            // it is possible we overwrite another thread's work here inadvertently.
            // pretty unlikely since this is scoped at the user level, but possible.
            // ideally the controller would start a transaction/lock before retrieving the data
            // then commit/unlock after the update. Or deep copy the user on retrieval and 
            // use TryUpdate to compare before updating.
            _users[user.UserId] = user;
            return ValueTask.FromResult(true);
        }

        public void Dispose() { }
    }
}
