using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Disuku.Discord.DisukuDiscord.Extensions
{
    public static class IOCServiceExtension
    {
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
