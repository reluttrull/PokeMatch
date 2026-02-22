using PokeMatch.Shared.Responses;
using StackExchange.Redis;

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
            var card = await response.Content.ReadFromJsonAsync<CardResponse>(cancellationToken: token);
            return card;
        }
    }
    public interface ICardApiClient
    {
        Task<CardResponse?> GetCardByIdAsync(string id, CancellationToken token = default);
    }
}
