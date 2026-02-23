using PokeMatch.Shared.Responses;
using StackExchange.Redis;
using System.Text.Json;

namespace DeckApi
{
    public class CardApiClient : ICardApiClient
    {
        private readonly HttpClient _client;
        private readonly IConnectionMultiplexer _redis;

        public CardApiClient(HttpClient client, IConnectionMultiplexer redis)
        {
            _client = client;
            _redis = redis;
        }

        public async Task<CardResponse?> GetCardByIdAsync(string id, CancellationToken token = default)
        {
            bool useCache = _redis.IsConnected;
            var db = useCache ? _redis.GetDatabase() : null;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            if (db?.KeyExists(id) ?? false)
            {
                System.Diagnostics.Debug.WriteLine($"Using cached data for card {id}");
                var cachedCardJson = db.StringGet(id);
                var cachedCard = JsonSerializer.Deserialize<CardResponse>(cachedCardJson.ToString(), options);
                if (cachedCard is not null) return cachedCard;
            }
            var response = await _client.GetAsync(id, token);
            if (!response.IsSuccessStatusCode)
            {
                var splitId = id.Split('-');
                string paddedId = splitId[1].PadLeft(3, '0');
                string fullPaddedId = $"{splitId[0]}-{paddedId}";
                response = await _client.GetAsync(fullPaddedId, token);
                if (!response.IsSuccessStatusCode) throw new HttpRequestException("failed to retrieve card data from TCGDex API");
            }
            response.EnsureSuccessStatusCode();
            var cardJson = await response.Content.ReadAsStringAsync(cancellationToken: token);
            db?.StringSet(id, cardJson);
            System.Diagnostics.Debug.WriteLine($"Writing to cache for card {id}");
            var card = JsonSerializer.Deserialize<CardResponse>(cardJson.ToString(), options);
            return card;
        }
    }
    public interface ICardApiClient
    {
        Task<CardResponse?> GetCardByIdAsync(string id, CancellationToken token = default);
    }
}
