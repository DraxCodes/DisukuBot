using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Disuku.Discord.DisukuDiscord.Extensions
{
    public static class IOCServiceExtension
    {
        public static async Task InitializeServicesAsync(this IServiceProvider services)
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => typeof(IDiscordServiceInitialize).IsAssignableFrom(x) && !x.IsInterface))
            {
                await ((IDiscordServiceInitialize)services.GetRequiredService(type)).InitializeAsync();
            }
        }
    }
}
