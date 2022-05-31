using System.Data;
using Example.VideoGameTracker.Api.Models;
using Example.VideoGameTracker.Api.Models.Request;

namespace Example.VideoGameTracker.Api.DataAccess
{
    public interface IUserDatabase : IDisposable
    {
        ValueTask<User?> AddNewAsync(UserRequest user, CancellationToken cancellationToken);

        ValueTask<User?> GetAsync(int userId, CancellationToken cancellationToken);

        ValueTask<bool> UpdateAsync(User user, CancellationToken cancellationToken);
    }
}
