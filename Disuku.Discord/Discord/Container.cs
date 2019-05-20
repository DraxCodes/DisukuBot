using Disuku.Core.Discord;
using Disuku.Core.Services.Logger;
using Disuku.Core.Services.RaiderIO;
using Disuku.Core.Services.TMDB;
using Disuku.Discord.Discord.Adapters;
using Disuku.MongoStorage;
using Microsoft.Extensions.DependencyInjection;

namespace Disuku.Discord
{
    public static class Container
    {
        public static IServiceCollection AddDisukuTypes(this IServiceCollection collection)
            => collection
            .AddSingleton<IMongoDbStorage, MongoDbStorage>()
            .AddSingleton<IDisukuLogger, DisukuLogger>()
            .AddSingleton<ITmdbService, TmdbService>()
            .AddSingleton<IRaiderIOService, RaiderIOService>()
            .AddTransient<IDiscordMessage, DiscordMessage>();
    }
}
