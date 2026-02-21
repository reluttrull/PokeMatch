using Microsoft.IdentityModel.Tokens;
using System.Collections;

namespace PokeMatch.Model
{
    public class Card
    {
        public int NumberInDeck { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
