using StackExchange.Redis;

namespace PokeMatch.Services
{
    public class CardApiService : ICardApiService
    {
        private readonly IConnectionMultiplexer _redis;
        public CardApiService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }
    }

    public interface ICardApiService
    {

    }
}
