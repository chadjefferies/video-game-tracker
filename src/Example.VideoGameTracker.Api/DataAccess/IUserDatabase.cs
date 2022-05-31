using System.Data;
using Example.VideoGameTracker.Api.Models;

namespace Example.VideoGameTracker.Api.DataAccess
{
    public interface IUserDatabase : IDisposable
    {
        Task<bool> AddNewAsync(User user);

        Task<User?> GetAsync(int userId);

        Task<bool> UpdateAsync(User user);
    }
}
