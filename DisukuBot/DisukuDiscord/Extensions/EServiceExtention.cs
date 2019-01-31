using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DisukuBot.DisukuDiscord.Extensions
{
    public static class EServiceExtention
    {
        public static IServiceCollection AutoAddServices(this IServiceCollection services)
        {
            foreach (var type in Assembly.GetCallingAssembly().GetTypes()
                .Where(x => typeof(IServiceExtention).IsAssignableFrom(x) && !x.IsInterface))
            {
                services.AddSingleton(type);
            }
            return services;
        }

        public static async Task InitializeServicesAsync(this IServiceProvider services)
        {
            foreach (var type in Assembly.GetCallingAssembly().GetTypes()
                .Where(x => typeof(IServiceExtention).IsAssignableFrom(x) && !x.IsInterface))
            {
                await ((IServiceExtention)services.GetRequiredService(type)).InitializeAsync();
            }
        }
    }
}
