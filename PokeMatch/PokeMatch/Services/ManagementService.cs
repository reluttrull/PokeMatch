using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Caching.Memory;
using PokeMatch.Components.Pages;
using PokeMatch.Extensions;
using PokeMatch.Model;
using PokeMatch.Shared.Responses;

namespace PokeMatch.Services
{
    public class ManagementService : IManagementService
    {
        private readonly IDeckClient _deckClient;
        private readonly IMemoryCache _gameCache;
        private int gameCacheTimeoutHours = 3;
        public ManagementService(IDeckClient deckClient, IMemoryCache gameCache)
        {
            _deckClient = deckClient;
            _gameCache = gameCache;
        }
        public async Task<GameState> StartGame(int userId, CancellationToken token = default)
        {
            if (userId == 0) // test data for now. todo: add player matching service
            {
                Player player1 = new() { UserId = 0, UserName = "Player" };
                Player player2 = new() { UserId = 1, UserName = "Opponent" };

                DeckResponse? deckResponse1 = await _deckClient.GetDeckByIdAsync(0);
                DeckResponse? deckResponse2 = await _deckClient.GetDeckByIdAsync(1);
                if (deckResponse1 is null || deckResponse2 is null) throw new NullReferenceException();

                GameState game = new(player1, player2, deckResponse1.MapFromResponse(), deckResponse2.MapFromResponse());
                game.InitializeGame();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(gameCacheTimeoutHours));
                _gameCache.Set<GameState>(game.Player1.Player.UserId, game, cacheEntryOptions);
                _gameCache.Set<GameState>(game.Player2.Player.UserId, game, cacheEntryOptions); // todo: only store once

                return game;
            }
            throw new NotImplementedException();
        }

        public bool TryReloadMatchForUser(int userId, out GameState? existingGame)
        {
            var success = _gameCache.TryGetValue(userId, out existingGame);
            return success;
        }
    }
    public interface IManagementService
    {
        public bool TryReloadMatchForUser(int userId, out GameState? existingGame);
        public Task<GameState> StartGame(int userId, CancellationToken token = default);
    }
}
