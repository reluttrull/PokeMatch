using PokeMatch.Shared.Responses;

namespace PokeMatch
{
    public class DeckClient : IDeckClient
    {
        private readonly HttpClient _client;

        public DeckClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<DeckResponse?> GetDeckByIdAsync(int deckId)
        {
            var response = await _client.GetAsync($"/api/decks/{deckId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<DeckResponse?>();
        }
    }
    public interface IDeckClient
    {
        Task<DeckResponse?> GetDeckByIdAsync(int deckId);
    }
}
