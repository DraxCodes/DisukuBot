using Discord.WebSocket;
using Disuku.Core.Discord;
using Disuku.Core.Entities;
using Disuku.Core.Entities.Embeds;
using Disuku.Discord.Converters;
using System;
using System.Text;
using System.Threading.Tasks;

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
            var channel = GetSocketTextChannel(chanId);
            await channel.SendMessageAsync(message);
        }

        public async Task SendDiscordMessageAsync(ulong chanId, DisukuUser user)
        {
            var formattedRoles = new StringBuilder();
            foreach (var role in user.Roles)
            {
                if (role.Name == "@everyone") { formattedRoles.Append($"{role.Name}, "); continue; }
                formattedRoles.Append($"<@&{role.Id}>, ");
            }

            var embed = new DisukuEmbed
            {
                Title = $"Profile: {user.Username}",
                Description =
                $"**User ID:** {user.UserId}\n" +
                $"**Guild ID:** {user.GuildId}\n" +
                $"**Account Created:** {user.CreatedAt}\n" +
                $"**Joined Server:** {user.JoinedAt}\n" +
                $"**Roles:**\n" +
                $"{formattedRoles}",
                Thumbnail = user.AvatarUrl
            };

            await SendDiscordEmbedAsync(chanId, embed);
        }

        public async Task SendDiscordMessageAsync(ulong chanId, DisukuGuild guild)
        {
            var embed = new DisukuEmbed
            {
                Title = $"Guild Profile: {guild.Name}",
                Thumbnail = guild.GuildAvatar,
                Description =
                $"**ID:** {guild.GuildId}\n" +
                $"**Channel Count:** \n" +
                $"⠀⠀▷ Text: {guild.TextChannelCount}, Voice: {guild.VoiceChannelCount}\n" +
                $"**Creation Date:** {guild.CreationDate}\n" +
                $"**Members:** {guild.MemberCount}"
            };

            await SendDiscordEmbedAsync(chanId, embed);
        }

        public async Task SendDiscordEmbedAsync(ulong chanId, DisukuEmbed embed)
        {
            var channel = GetSocketTextChannel(chanId);

            var discordEmbed = DisukuEntityConverter.ConvertEmbed(embed);
            await channel.SendMessageAsync(embed: discordEmbed);
        }

        private SocketTextChannel GetSocketTextChannel(ulong chanId)
        {
            var channel = _client.GetChannel(chanId) as SocketTextChannel;
            if (channel is null) { throw new InvalidCastException($"Unable to cast: {typeof(SocketChannel)} to {typeof(SocketTextChannel)} in {GetType().Name} "); }
            return channel;
        }

    }
}
