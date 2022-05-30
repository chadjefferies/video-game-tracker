using Example.VideoGameTracker.Api.DataAccess;
using Microsoft.Extensions.Options;

namespace Example.VideoGameTracker.Api
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddVideoGameDatabase(this IServiceCollection services)
        {
            services.AddHttpClient<IVideoGameDatabase, RawgVideoGameDatabase>((sp, client) =>
            {
                var opt = sp.GetRequiredService<IOptions<RawgOptions>>();
                client.BaseAddress = new Uri(opt.Value.ApiUrl);
            })
            .SetHandlerLifetime(Timeout.InfiniteTimeSpan);
            return services;
        }

        public static IServiceCollection AddUserDatabase(this IServiceCollection services)
        {
            services.AddSingleton<IUserDatabase, InMemoryUserDatabase>();
            return services;
        }
    }
}
