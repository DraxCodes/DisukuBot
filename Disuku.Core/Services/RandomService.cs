using Disuku.Core.Discord;
using System;

namespace Disuku.Core.Services
{
    public class RandomService
    {
        private IDiscordEvents _discordEvents;
        public RandomService(IDiscordEvents discordEvents)
        {
            _discordEvents = discordEvents;
        }

        public void Initialize()
        {
            _discordEvents.ClientReady += ClientReadyEvent;
        }

        private void ClientReadyEvent(object sender, EventArgs e)
        {
            Console.WriteLine("TEST WORKED");
        }
    }
}
