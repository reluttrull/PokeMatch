using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokeMatch.Shared.Requests;
using PokeMatch.Shared.Responses;

namespace DeckApi.Controllers
{
    [Route("api/decks")]
    [ApiController]
    public class DeckController : ControllerBase
    {
        [HttpGet]
        [Route("/public")]
        public async Task<List<DeckBrief>> GetPublicDeckBriefs(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("/me")]
        public async Task<List<DeckBrief>> GetMyDeckBriefs(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("/")]
        public async Task<IActionResult> Create([FromBody] CreateDeckRequest request, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
