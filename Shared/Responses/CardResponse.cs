using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PokeMatch.Shared.Responses
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "cardType")]
    [JsonDerivedType(typeof(PokemonCardResponse), "pokemon")]
    [JsonDerivedType(typeof(EnergyCardResponse), "energy")]
    [JsonDerivedType(typeof(TrainerCardResponse), "trainer")]
    public abstract record CardResponse(
        int NumberInDeck,
        string Category,
        string Id,
        string Image,
        string Name,
        string Description
    );

    public record PokemonCardResponse(
        int NumberInDeck,
        string Category,
        string Id,
        string Image,
        string Name,
        string Description,
        int Hp,
        List<string> Types,
        string EvolveFrom,
        string Stage,
        //List<AbilityDto> Abilities,
        //List<AttackDto> Attacks,
        //List<WeaknessDto> Weaknesses,
        //List<ResistanceDto> Resistances,
        int RetreatCost
    ) : CardResponse(NumberInDeck, Category, Id, Image, Name, Description);

    public record EnergyCardResponse(
        int NumberInDeck,
        string Category,
        string Id,
        string Image,
        string Name,
        string Description,
        string Effect,
        string EnergyType,
        string EnergyColor
    ) : CardResponse(NumberInDeck, Category, Id, Image, Name, Description);

    public record TrainerCardResponse(
        int NumberInDeck,
        string Category,
        string Id,
        string Image,
        string Name,
        string Description,
        string Effect,
        string TrainerType
    ) : CardResponse(NumberInDeck, Category, Id, Image, Name, Description);
}
