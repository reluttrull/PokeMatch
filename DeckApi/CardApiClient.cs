using PokeMatch.Shared.Responses;
using System.Text.Json;
using ZiggyCreatures.Caching.Fusion;

namespace DeckApi
{
    public class CardApiClient : ICardApiClient
    {
        private readonly HttpClient _client;
        private readonly IFusionCache _cache;

        public CardApiClient(HttpClient client, IFusionCache cache)
        {
            _client = client;
            _cache = cache;
        }

        public async Task<CardResponse?> GetCardByIdAsync(string id, CancellationToken token = default)
        {
            // todo: clean up this method
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var maybeCachedCard = await _cache.TryGetAsync<string>(id, token: token);
            if (maybeCachedCard.HasValue)
            {
                System.Diagnostics.Debug.WriteLine($"Using cached data for card {id}");
                var cachedCard = JsonSerializer.Deserialize<CardResponse>(maybeCachedCard.Value, options);
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
            if (cardJson is null) return null;

            _cache.Set(id, cardJson, token: token);
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
