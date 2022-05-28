using System.Net.Http.Headers;

namespace Example.VideoGameTracker.Api
{
    public static class HttpRequestMessageExtensions
    {
        public static HttpRequestMessage WithUserAgent(this HttpRequestMessage request)
        {
            request.Headers.UserAgent.Add(new ProductInfoHeaderValue("VideoGameTracker", "1.0"));
            request.Headers.UserAgent.Add(new ProductInfoHeaderValue("(+https://github.com/chadjefferies/video-game-tracker)"));
            return request;
        }
    }
}
