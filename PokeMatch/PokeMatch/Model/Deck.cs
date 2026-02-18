using System.Text.Json.Serialization;

namespace PokeMatch.Model
{
    public class Deck
    {
        public int DeckId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsDefault { get; set; } = false;
        public List<string> CardIds { get; set; } = [];
        [JsonIgnore]
        public List<Card> Cards { get; set; } = [];
    }
}
