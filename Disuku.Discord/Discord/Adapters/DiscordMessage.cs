using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Disuku.Core.Discord;
using Disuku.Core.Entities.Embeds;
using Disuku.Discord.Converters;

namespace Disuku.Discord.Discord.Adapters
{
    public class DiscordMessage : IDiscordMessage
    {
        private DiscordSocketClient _client;

        public DiscordMessage(DiscordSocketClient client)
        {
            _client = client;
        }

        public async Task SendDiscordMessageAsync(ulong chanId, string message)
        {
            var channel = _client.GetChannel(chanId) as SocketTextChannel;
            if(channel is null) { throw new InvalidCastException($"Unable to cast: {typeof(SocketChannel)} to {typeof(SocketTextChannel)} in {GetType().Name} " ); }

            await channel.SendMessageAsync(message);
        }

        public async Task SendDiscordEmbedAsync(ulong chanId, DisukuEmbed embed)
        {
            var channel = _client.GetChannel(chanId) as SocketTextChannel;
            if (channel is null) { throw new InvalidCastException($"Unable to cast: {typeof(SocketChannel)} to {typeof(SocketTextChannel)} in {GetType().Name} "); }

            var discordEmbed = DisukuEntityConverter.ConvertEmbed(embed);
            await channel.SendMessageAsync(embed: discordEmbed);
        }
    }
}
