using Disuku.Core.Services.Logger;
using Disuku.Core.Services.RaiderIO;
using Disuku.Core.Services.TMDB;
using Disuku.MongoStorage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Disuku.InversionOfControl
{
    public static class Container
    {
        public static IServiceCollection AddDisukuTypes(this IServiceCollection collection)
            => collection
            .AddSingleton<IMongoDbStorage, MongoDbStorage>()
            .AddSingleton<IDisukuLogger, DisukuLogger>()
            .AddSingleton<ITmdbService, TmdbService>()
            .AddSingleton<IRaiderIOService, RaiderIOService>();
    }
}
