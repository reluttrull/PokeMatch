using PokeMatch.Model;

namespace PokeMatch.Extensions
{
    public static class CardExtensions
    {
        public static bool IsBasicPokemonCard(this Card card)
        {
            return card is PokemonCard && ((PokemonCard)card).Stage == "Basic";
        }
    }
}
