using PokeMatch.Model;

namespace PokeMatch.Extensions
{
    public static class GameStateExtensions
    {
        public static void InitializeGame(this GameState game)
        {
            // player 1
            game.Player1.DrawStartingHand();
            game.Player2.DrawStartingHand();
        }
    }
}
