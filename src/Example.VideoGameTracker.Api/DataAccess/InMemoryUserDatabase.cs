using System.Collections.Concurrent;
using System.Data;
using Example.VideoGameTracker.Api.Models;

/********************************************************
 * Async in this class is overhead, but this is an implementation of IUserDatabase, 
 * which could be implemented for other backend datasources beyond local memory, 
 * where network calls are highly likely
 ********************************************************/

namespace Example.VideoGameTracker.Api.DataAccess
{
    public class InMemoryUserDatabase : IUserDatabase
    {
        private readonly ConcurrentDictionary<int, User> _users;

        public InMemoryUserDatabase()
        {
            _users = new ConcurrentDictionary<int, User>();
        }

        public Task<IDbTransaction> BeginTransactionAsync()
        {
            return Task.FromResult<IDbTransaction>(new InMemoryTransaction());
        }

        public Task<bool> AddNewAsync(User user)
        {
            return Task.FromResult(_users.TryAdd(user.UserId, user));
        }

        public Task<User> GetAsync(int userId)
        {
            if (_users.TryGetValue(userId, out var user))
            {
                return Task.FromResult(user);
            }

            return Task.FromResult<User>(default);
        }

        public Task<bool> UpdateAsync(User user)
        {
            // since we are doing a blind update here
            // it is possible we overwrite another thread's work here inadvertently
            // ideally the caller would start a transaction before retrieving the data
            // then commit after the update. Or deep copy the user on retrieval and 
            // use TryUpdate to compare before updating.
            _users[user.UserId] = user;
            return Task.FromResult(true);
        }

        public void Dispose() { }
    }
}
