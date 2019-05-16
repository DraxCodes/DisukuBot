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
            => collection.AddSingleton<IMongoDbStorage, MongoDbStorage>();

        public static IServiceCollection AutoAddServices(this IServiceCollection services)
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => typeof(IServiceExtention).IsAssignableFrom(x) && !x.IsInterface))
            {
                services.AddSingleton(type);
            }
            return services;
        }

        public static async Task InitializeServicesAsync(this IServiceProvider services)
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => typeof(IServiceExtention).IsAssignableFrom(x) && !x.IsInterface))
            {
                await ((IServiceExtention)services.GetRequiredService(type)).InitializeAsync();
            }
        }

    }
}
