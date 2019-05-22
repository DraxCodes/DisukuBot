using Disuku.Core.Discord;
using Disuku.Core.Providers.Profile;
using Disuku.Core.Services;
using Disuku.Core.Services.DisukuProfiles;
using Disuku.Core.Services.Logger;
using Disuku.Core.Services.RaiderIO;
using Disuku.Core.Services.TMDB;
using Disuku.Core.Storage;
using Disuku.Discord.Discord.Adapters;
using Disuku.Discord.Discord.Events;
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
            .AddTransient<IDiscordMessage, DiscordMessage>()
            .AddSingleton<IDiscordEvents, DiscordEvents>()
            .AddSingleton<UserProfileService>()
            .AddSingleton<DisukuUserProvider>()
            .AddSingleton<DisukuGuildProvider>()
            .AddSingleton<GuildProfileService>()
            .AddSingleton<RandomService>();
    }
}
