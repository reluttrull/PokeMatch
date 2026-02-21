using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokeMatch.Shared.Requests;
using PokeMatch.Shared.Responses;
using System.Runtime.Serialization;
using System.Text.Json;

namespace DeckApi.Controllers
{
    [ApiController]
    public class DeckController : ControllerBase
    {
        [HttpGet]
        [Route("/api/decks/public")]
        public async Task<List<DeckBrief>> GetPublicDeckBriefs(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/api/decks/me")]
        public async Task<List<DeckBrief>> GetMyDeckBriefs(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/api/decks/{id}")]
        public async Task<DeckResponse?> GetById(int id, CancellationToken token)
        {
            using StreamReader r = new("Data/TestDecks.json");
            // todo: switch to repository
            string json = r.ReadToEnd();
            List<DeckResponse>? decks = JsonSerializer.Deserialize<List<DeckResponse>>(json);
            if (decks is null) throw new SerializationException($"Problem deserializing deck data.");
            DeckResponse? deck = decks.FirstOrDefault(d => d.DeckId == id);
            if (deck is null) return null;
            return deck;
        }

        [HttpPost]
        [Route("/api/decks/")]
        public async Task<IActionResult> Create([FromBody] CreateDeckRequest request, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("/api/decks/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
