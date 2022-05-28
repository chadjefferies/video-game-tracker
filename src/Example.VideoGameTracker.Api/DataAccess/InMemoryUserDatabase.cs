using System.Collections.Concurrent;
using Example.VideoGameTracker.Api.Models;

/********************************************************
 * Async in this class is overhead, but this is an implementation of I, which could be implemented for other backend datasources beyond local memory, 
 * where network calls are highly likely
 ********************************************************/

namespace Example.VideoGameTracker.Api.DataAccess
{
    public class InMemoryUserDatabase // TODO: interface
    {
        private readonly ConcurrentDictionary<int, User> _users;
        private readonly ReaderWriterLockSlim _slimLock;

        public InMemoryUserDatabase()
        {
            _users = new ConcurrentDictionary<int, User>(2, 1000);
            _slimLock = new ReaderWriterLockSlim();
        }

        public Task<bool> AddNewUserAsync(User user)
        {
            return Task.FromResult(_users.TryAdd(user.UserId, user));
        }

        public Task<User> GetUserAsync(int userId)
        {
            if (_users.TryGetValue(userId, out var user))
            {
                return Task.FromResult(user);
            }

            return Task.FromResult<User>(default);
        }

        public Task<bool> UpdateUser(User newUser, User oldUser)
        {
            return Task.FromResult(_users.TryUpdate(newUser.UserId, newUser, oldUser));
        }
    }
}
