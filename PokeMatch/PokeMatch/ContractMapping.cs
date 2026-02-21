using PokeMatch.Model;
using PokeMatch.Shared.Responses;

namespace PokeMatch
{
    public static class ContractMapping
    {
        public static Deck MapFromResponse(this DeckResponse response)
        {
            Deck deck = new()
            {
                DeckId = response.DeckId,
                Name = response.Name,
                Description = response.Description ?? string.Empty,
                IsDefault = response.IsDefault,
                Cards = response.Cards.Select(cr => cr.MapFromResponse()).ToList()
            };
            return deck;
        }
        public static Card MapFromResponse(this CardResponse response)
        {
            return response switch
            {
                PokemonCardResponse p => p.MapFromResponse(),
                EnergyCardResponse e => e.MapFromResponse(),
                TrainerCardResponse t => t.MapFromResponse(),
                _ => throw new NotSupportedException($"Unknown card response type: {response.GetType().Name}")
            };
        }
        public static EnergyCard MapFromResponse(this EnergyCardResponse response)
        {
            EnergyCard card = new()
            {
                NumberInDeck = response.NumberInDeck,
                Category = response.Category,
                Id = response.Id,
                Image = response.Image,
                Name = response.Name,
                Description = response.Description,
                Effect = response.Effect,
                EnergyType = response.EnergyType,
                EnergyColor = response.EnergyColor
            };
            return card;
        }
        public static PokemonCard MapFromResponse(this PokemonCardResponse response)
        {
            PokemonCard card = new()
            {
                NumberInDeck = response.NumberInDeck,
                Category = response.Category,
                Id = response.Id,
                Image = response.Image,
                Name = response.Name,
                Description = response.Description,
                Hp = response.Hp,
                Types = response.Types,
                EvolveFrom = response.EvolveFrom,
                Stage = response.Stage,
                RetreatCost = response.RetreatCost
            };
            return card;
        }
        public static TrainerCard MapFromResponse(this TrainerCardResponse response)
        {
            TrainerCard card = new()
            {
                NumberInDeck = response.NumberInDeck,
                Category = response.Category,
                Id = response.Id,
                Image = response.Image,
                Name = response.Name,
                Description = response.Description,
                Effect = response.Effect,
                TrainerType = response.TrainerType
            };
            return card;
        }
    }
}
