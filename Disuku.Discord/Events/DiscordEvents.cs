using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using Disuku.Core.Discord;

namespace Disuku.Discord.Events
{
    public class DiscordEvents : IDiscordEvents
    {
        private readonly DiscordSocketClient _client;
        public event EventHandler ClientReady;

        public DiscordEvents(DiscordSocketClient client)
        {
            _client = client;
            _client.Ready += ClientReadyEvent;
        }

        private Task ClientReadyEvent()
        {
            ClientReady?.Invoke(this, EventArgs.Empty);
            return Task.CompletedTask;
        }
    }
}
