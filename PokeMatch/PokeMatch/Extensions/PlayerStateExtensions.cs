using PokeMatch.Model;

namespace PokeMatch.Extensions
{
    public static class PlayerStateExtensions
    {
        public static void DrawStartingHand(this PlayerState player)
        {
            if (!player.Deck.Cards.Any(c => c.IsBasicPokemonCard()))
                throw new ArgumentException("No basic Pokemon in deck!");
            while (true)
            {
                var random = new Random();
                var testHand = player.Deck.Cards.OrderBy(c => random.Next()).Take(7).ToList();
                if (testHand.Any(c => c.IsBasicPokemonCard()))
                {
                    player.Hand = testHand;
                    break;
                }
                player.MulliganHands.Add(testHand);
                player.Mulligans++;
            }
            player.Deck.Cards = [.. player.Deck.Cards.Except(player.Hand)];
        }
    }
}
