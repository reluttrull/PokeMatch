using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PokeMatch.Shared.Responses
{
    public class DeckResponse
    {
        public required int DeckId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsDefault { get; set; }
        public List<string> CardIds { get; set; } = [];
        public List<CardResponse> Cards { get; set; } = [];
    }
}
