using System.Runtime.CompilerServices;
using System.Text.Json;
using Example.VideoGameTracker.Api.Models;
using Microsoft.Extensions.Options;

namespace Example.VideoGameTracker.Api.DataAccess
{
    public class RawgVideoGameDatabase : IVideoGameDatabase
    {
        private readonly HttpClient _httpClient;
        private readonly RawgOptions _options;

        public RawgVideoGameDatabase(HttpClient httpClient, IOptions<RawgOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async IAsyncEnumerable<Game> GetGamesAsync(string search, string ordering, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"games?key={_options.ApiKey}");

            using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            response.EnsureSuccessStatusCode();

       //     var rr = await response.Content.ReadAsStringAsync(cancellationToken);

            using var responseData = await response.Content.ReadAsStreamAsync(cancellationToken);

            var gameData = JsonSerializer.DeserializeAsyncEnumerable<dynamic>(
                responseData, 
                new JsonSerializerOptions 
                { 
                    PropertyNameCaseInsensitive = true, 
                    DefaultBufferSize = 128 
                }, 
                cancellationToken);
            await foreach (var game in gameData)
            {
                yield return game;
            }
        }

        public async Task<Game> GetGameAsync(int gameId, CancellationToken cancellationToken)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, $"games/{gameId}?key={_options.ApiKey}");

            using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

            response.EnsureSuccessStatusCode();

            var rr = await response.Content.ReadAsStringAsync(cancellationToken);

            throw new NotImplementedException(rr);
        }
    }
}
