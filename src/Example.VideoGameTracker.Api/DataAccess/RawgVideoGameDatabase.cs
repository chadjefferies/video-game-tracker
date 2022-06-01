using System.Net;
using System.Text.Json;
using Example.VideoGameTracker.Api.Extensions;
using Example.VideoGameTracker.Api.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

/********************************************************
 * This class uses raw HTTP to interact with the RAWG API. If this were a true integration, it would be best to use an OpenAPI generated client.
 * However for the purposes of this demo, I felt it was best to craft the API interactions manually since the api methods called were limited to just two.
 ********************************************************/

namespace Example.VideoGameTracker.Api.DataAccess
{
    public class RawgVideoGameDatabase : IVideoGameDatabase
    {
        private static readonly JsonSerializerOptions SERIALIZER_OPTIONS = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly HttpClient _httpClient;
        private readonly RawgOptions _options;
        private readonly IDistributedCache _cache;

        public RawgVideoGameDatabase(HttpClient httpClient, IOptions<RawgOptions> options, IDistributedCache cache)
        {
            _httpClient = httpClient;
            _options = options.Value;
            _cache = cache;
        }

        public async Task<IEnumerable<Game>?> GetGamesAsync(string? search, string? ordering, CancellationToken cancellationToken)
        {
            var queryParams = new Dictionary<string, string?>(4)
            {
                { "key", _options.ApiKey },
                { "lang", "en" },
                { "page_size", "20" }, // we are only returning the first page of 20 results.
                { "search", search }
            };

            if (!string.IsNullOrWhiteSpace(ordering))
            {
                queryParams.Add("ordering", ordering);
            }

            var query = QueryHelpers.AddQueryString("games", queryParams);

            var cachedResponse = await _cache.GetStringAsync(query, cancellationToken);

            if (cachedResponse != null)
            {
                var cachedGameData = JsonSerializer.Deserialize<GamesResponse>(
                    cachedResponse,
                     SERIALIZER_OPTIONS);
                if (cachedGameData != null)
                {
                    return cachedGameData.Results;
                }
            }

            using var request = new HttpRequestMessage(HttpMethod.Get, query)
                .WithUserAgent();

            using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            response.EnsureSuccessStatusCode();

            // ReadAsStream would be more efficient here from an allocation standpoint, however we need the string to cache it anyways
            var responseData = await response.Content.ReadAsStringAsync(cancellationToken);

            var gameData = JsonSerializer.Deserialize<GamesResponse>(
                responseData,
                SERIALIZER_OPTIONS);

            if (gameData?.Results != null)
            {
                await _cache.SetStringAsync(
                    query,
                    responseData,
                    new DistributedCacheEntryOptions
                    {
                        // reduce load on Rawg slightly
                        AbsoluteExpirationRelativeToNow = _options.ApiResponeCacheDuration
                    },
                    cancellationToken);

                return gameData.Results;
            }

            return Enumerable.Empty<Game>();
        }

        public async Task<Game?> GetGameAsync(int gameId, CancellationToken cancellationToken)
        {
            var queryParams = new Dictionary<string, string?>(2)
            {
                { "key", _options.ApiKey },
                { "lang", "en" }
            };

            var query = QueryHelpers.AddQueryString($"games/{gameId}", queryParams);

            var cachedResponse = await _cache.GetStringAsync(query, cancellationToken);

            if (cachedResponse != null)
            {
                var cachedGameData = JsonSerializer.Deserialize<Game>(
                    cachedResponse,
                     SERIALIZER_OPTIONS);
                if (cachedGameData != null)
                {
                    return cachedGameData;
                }
            }

            using var request = new HttpRequestMessage(HttpMethod.Get, query)
                .WithUserAgent();

            using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return default;
            }

            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync(cancellationToken);

            var gameData = JsonSerializer.Deserialize<Game>(
                responseData,
                SERIALIZER_OPTIONS);

            if (gameData != null)
            {
                await _cache.SetStringAsync(
                    query,
                    responseData,
                    new DistributedCacheEntryOptions
                    {
                        // reduce load on Rawg slightly
                        AbsoluteExpirationRelativeToNow = _options.ApiResponeCacheDuration
                    },
                    cancellationToken);

                return gameData;
            }

            return default;
        }
    }
}
