using System.Collections.Concurrent;
using Example.VideoGameTracker.Api.Models;

namespace Example.VideoGameTracker.Api.DataAccess
{
    public class InMemoryUserDatabase // TODO: interface
    {
        private readonly ConcurrentDictionary<int, User> _users;

        public InMemoryUserDatabase()
        {
            _users = new ConcurrentDictionary<int, User>(2, 1000);
        }

        public bool AddNewUser(User user)
        {
            return _users.TryAdd(user.UserId, user);
        }

        public User GetUser(int userId)
        {
            if (_users.TryGetValue(userId, out var user))
            {
                return user;
            }

            return default;
        }

        public bool UpdateUser(User newUser, User oldUser)
        {
            return _users.TryUpdate(newUser.UserId, newUser, oldUser);
        }
    }
}
