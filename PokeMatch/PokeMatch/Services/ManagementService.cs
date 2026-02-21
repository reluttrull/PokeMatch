using PokeMatch.Model;
using PokeMatch.Shared.Responses;

namespace PokeMatch.Services
{
    public class ManagementService : IManagementService
    {
        private readonly IDeckClient _deckClient;
        public ManagementService(IDeckClient deckClient)
        {
            _deckClient = deckClient;
        }
        public Task StartGame(int deckId, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Game> TryReloadMatchForUser(int userId, CancellationToken token = default)
        {
            if (userId == 0) // test data for now
            {
                Player player1 = new() { UserId = 0, UserName = "Player" };
                Player player2 = new() { UserId = 1, UserName = "Opponent" };

                DeckResponse? deck1 = await _deckClient.GetDeckByIdAsync(0);
                DeckResponse? deck2 = await _deckClient.GetDeckByIdAsync(1);
                if (deck1 is null || deck2 is null) throw new NullReferenceException();
                Game game = new(player1, player2, deck1.MapFromResponse(), deck2.MapFromResponse());
                return game;
            }
            throw new NotImplementedException();
        }
    }
    public interface IManagementService
    {
        public Task<Game> TryReloadMatchForUser(int userId, CancellationToken token = default);
        public Task StartGame(int deckId, CancellationToken token = default);
    }
}
