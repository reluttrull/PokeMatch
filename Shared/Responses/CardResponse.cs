using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PokeMatch.Shared.Responses
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "category")]
    [JsonDerivedType(typeof(CardResponse), "")]
    [JsonDerivedType(typeof(PokemonCardResponse), typeDiscriminator: "Pokemon")]
    [JsonDerivedType(typeof(TrainerCardResponse), typeDiscriminator: "Trainer")]
    [JsonDerivedType(typeof(EnergyCardResponse), typeDiscriminator: "Energy")]
    public class CardResponse
    {
        public int NumberInDeck { get; set; }
        [JsonPropertyName("category")]
        [JsonIgnore]
        public string Category { get; set; } = string.Empty;
        public required string Id { get; set; }
        public required string Image { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class PokemonCardResponse : CardResponse
    {
        public int Hp { get; set; }
        public List<string> Types { get; set; } = [];
        public string EvolveFrom { get; set; } = string.Empty;
        public string Stage { get; set; } = string.Empty;
        public int RetreatCost { get; set; }
        //List<AbilityDto> Abilities,
        //List<AttackDto> Attacks,
        //List<WeaknessDto> Weaknesses,
        //List<ResistanceDto> Resistances,
    }

    public class EnergyCardResponse : CardResponse
    {
        public string Effect { get; set; } = string.Empty;
        public string EnergyType { get; set; } = string.Empty;
        public string EnergyColor { get; set; } = string.Empty;
    }

    public class TrainerCardResponse : CardResponse
    {
        public string Effect { get; set; } = string.Empty;
        public string TrainerType { get; set; } = string.Empty;
    }
}
