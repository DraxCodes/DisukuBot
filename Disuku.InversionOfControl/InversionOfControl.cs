﻿using Disuku.Core.Providers.Profile;
using Disuku.Core.Services.DisukuProfiles;
using Disuku.Core.Services.Logger;
using Disuku.Core.Services.Quotes;
using Disuku.Core.Services.RaiderIO;
using Disuku.Core.Services.TMDB;
using Disuku.Core.Services.Todo;
using Disuku.Core.Storage;
using Disuku.MongoStorage;
using Microsoft.Extensions.DependencyInjection;

namespace Disuku.InversionOfControl
{
    public static class InversionOfControl
    {
        public static IServiceCollection AddDisukuTypes(this IServiceCollection collection)
            => collection
            .AddTransient<IDataStore, MongoDbStorage>()
            .AddSingleton<IDisukuLogger, DisukuLogger>()
            .AddSingleton<ITmdbService, TmdbService>()
            .AddSingleton<IRaiderIOService, RaiderIOService>()
            .AddSingleton<ITodoService, TodoService>()
            .AddTransient<IQuoteService, QuoteService>()
            .AddSingleton<UserProfileService>()
            .AddSingleton<DisukuGuildProvider>()
            .AddSingleton<GuildProfileService>();
    }
}
