using Microsoft.Extensions.Options;

namespace Example.VideoGameTracker.Api
{
    public class RawgOptions : IOptions<RawgOptions>
    {
        public string ApiUrl { get; set; }

        public string ApiKey { get; set; }

        public RawgOptions Value => this;
    }
}
