using Microsoft.Extensions.Options;

namespace Example.VideoGameTracker.Api
{
    public class ConfigureRawgOptions : IConfigureOptions<RawgOptions>
    {
        private readonly IConfiguration _configuration;

        public ConfigureRawgOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(RawgOptions options)
        {
            _configuration.Bind("Rawg", options);
        }
    }
}
