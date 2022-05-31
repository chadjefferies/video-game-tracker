using System.Data;
using Example.VideoGameTracker.Api.Models;

namespace Example.VideoGameTracker.Api.DataAccess
{
    public interface IUserDatabase : IDisposable
    {
        Task<User?> AddNewAsync(UserRequest user);

        Task<User?> GetAsync(int userId);

        Task<bool> UpdateAsync(User user);
    }
}
