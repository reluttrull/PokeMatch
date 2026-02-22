using PokeMatch.Services;
using StackExchange.Redis;

namespace PokeMatch.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IManagementService, ManagementService>();
            services.AddScoped<IGameService, GameService>();

            return services;
        }
    }
}
