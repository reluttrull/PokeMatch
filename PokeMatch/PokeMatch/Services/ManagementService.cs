using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Caching.Memory;
using PokeMatch.Components.Pages;
using PokeMatch.Extensions;
using PokeMatch.Model;
using PokeMatch.Shared.Responses;
using System.Security.Cryptography;

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
            string joinCode = RandomNumberGenerator.GetHexString(8);

            Player player1 = new() { UserId = userId, UserName = "Player" };
            Player player2 = new() { UserId = -1, UserName = "Opponent" };

            DeckResponse? deckResponse1 = await _deckClient.GetDeckByIdAsync(0);
            DeckResponse? deckResponse2 = await _deckClient.GetDeckByIdAsync(1);
            if (deckResponse1 is null || deckResponse2 is null) throw new NullReferenceException();

            GameState game = new(joinCode, player1, player2, deckResponse1.MapFromResponse(), deckResponse2.MapFromResponse());
            game.InitializeGame();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(gameCacheTimeoutHours));
            _gameCache.Set<GameState>(game.Player1.Player.UserId, game, cacheEntryOptions);
            _gameCache.Set<GameState>(joinCode, game, cacheEntryOptions);

            return game;
        }

        public async Task<GameState?> JoinGame(int userId, string joinCode, CancellationToken token = default)
        {
            var success = _gameCache.TryGetValue<GameState>(joinCode, out var game);
            if (!success || game is null) return null;

            game.Player2.Player.UserId = userId;

            _gameCache.Remove(joinCode);
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(gameCacheTimeoutHours));
            _gameCache.Set<GameState>(userId, game, cacheEntryOptions);
            _gameCache.Set<GameState>(game.Player1.Player.UserId, game, cacheEntryOptions); // todo: only store once

            return game;
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
        public Task<GameState?> JoinGame(int userId, string accessCode, CancellationToken token = default);
    }
}
