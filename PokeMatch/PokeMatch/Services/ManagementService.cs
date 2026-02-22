using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        public async Task<Game> StartGame(int userId, CancellationToken token = default)
        {
            if (userId == 0) // test data for now
            {
                Player player1 = new() { UserId = 0, UserName = "Player" };
                Player player2 = new() { UserId = 1, UserName = "Opponent" };

                DeckResponse? deckResponse1 = await _deckClient.GetDeckByIdAsync(0);
                DeckResponse? deckResponse2 = await _deckClient.GetDeckByIdAsync(1);
                if (deckResponse1 is null || deckResponse2 is null) throw new NullReferenceException();

                Game game = new(player1, player2, deckResponse1.MapFromResponse(), deckResponse2.MapFromResponse());

                return game;
            }
            throw new NotImplementedException();
        }

        public async Task<Game> TryReloadMatchForUser(int userId, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
    public interface IManagementService
    {
        public Task<Game> TryReloadMatchForUser(int userId, CancellationToken token = default);
        public Task<Game> StartGame(int userId, CancellationToken token = default);
    }
}
