namespace PokeMatch.Services
{
    public class ManagementService : IManagementService
    {
        public Task StartGame(int deckId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> TryReloadMatch(Guid gameId)
        {
            throw new NotImplementedException();
        }
    }
    public interface IManagementService
    {
        public Task<bool> TryReloadMatch(Guid gameId);
        public Task StartGame(int deckId);
    }
}
