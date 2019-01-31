using DisukuBot.DisukuCore;
using DisukuBot.DisukuCore.Interfaces;
using DisukuBot.DisukuCore.Services;
using DisukuBot.DisukuDiscord;
using Microsoft.Extensions.DependencyInjection;

namespace DisukuConsoleApp
{
    public static class InversionOfControl
    {
        private static ServiceProvider provider;

        public static ServiceProvider Provider
        {
            get
            {
                return GetOrInitProvider();
            }
        }

        private static ServiceProvider GetOrInitProvider()
        {
            if (provider is null)
            {
                InitializeProvider();
            }

            return provider;
        }

        private static void InitializeProvider()
            => provider = new ServiceCollection()
            .AddSingleton<IDisukuBotClient, DisukuBotClient>()
            .AddSingleton<IWorldOfWarcraftService, WorldOfWarcraftService>()
            .BuildServiceProvider();
    }
}
