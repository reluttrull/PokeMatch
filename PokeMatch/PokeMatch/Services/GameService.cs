using PokeMatch.Model;

namespace PokeMatch.Services
{
    public class GameService : IGameService
    {
        public bool CanMoveCard(Card movingCard, PlaySpot toSpot)
        {
            return false;
        }

        public bool CanMoveCard(PlaySpot fromSpot, PlaySpot toSpot)
        {
            return false;
        }

        public async Task MoveCard(Card movingCard, PlaySpot toSpot, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }

    public interface IGameService
    {
        public bool CanMoveCard(Card movingCard, PlaySpot toSpot);
        public bool CanMoveCard(PlaySpot fromSpot, PlaySpot toSpot);
        public Task MoveCard(Card movingCard, PlaySpot toSpot, CancellationToken cancellationToken = default);
    }
}
