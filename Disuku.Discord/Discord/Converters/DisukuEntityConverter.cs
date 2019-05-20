using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Disuku.Core.Entities.Embeds;
using Disuku.Core.Entities.Logging;

namespace Disuku.Discord.Converters
{
    public static class DisukuEntityConverter
    {

        public static Embed ConvertEmbed(DisukuEmbed embed, bool inlineFields = false)
        {
            var discordEmbed = new EmbedBuilder()
                .WithTitle(embed.Title)
                .WithDescription(embed.Description)
                .WithCurrentTimestamp()
                .WithColor(Color.Blue);

            if (!string.IsNullOrWhiteSpace(embed.URL)) { discordEmbed.WithUrl(embed.URL); }
            if (!string.IsNullOrWhiteSpace(embed.Thumbnail)) { discordEmbed.WithThumbnailUrl(embed.Thumbnail); }
            if (!string.IsNullOrWhiteSpace(embed.Footer)) { discordEmbed.WithFooter(embed.Footer); }
            if (!string.IsNullOrWhiteSpace(embed.ImageUrl)) { discordEmbed.WithImageUrl(embed.ImageUrl); }

            if (embed.Fields.Count != 0)
            {
                foreach (var field in embed.Fields)
                {
                    discordEmbed.AddField(field.Title, field.Description, inlineFields);
                }
            }

            return discordEmbed.Build();
        }

        public static DisukuLog CovertLog(LogMessage logMessage)
            => new DisukuLog
            {
                Message = logMessage.Message,
                Source = logMessage.Source,
                Severity = ConvertSevrity(logMessage.Severity)
            };

        private static DisukuLogSeverity ConvertSevrity(LogSeverity severity)
        {
            switch (severity)
            {
                case LogSeverity.Critical:
                    return DisukuLogSeverity.Critical;
                case LogSeverity.Error:
                    return DisukuLogSeverity.Error;
                case LogSeverity.Warning:
                    return DisukuLogSeverity.Warning;
                case LogSeverity.Info:
                    return DisukuLogSeverity.Info;
                default:
                    return DisukuLogSeverity.Info;
            }
        }

        public static DisukuCommandLog ConvertCommandLog(SocketGuild guild, SocketGuildChannel channel, SocketGuildUser user, CommandInfo command)
            => new DisukuCommandLog
            {
                Channel = channel.Name,
                User = $"{user.Username}#{user.Discriminator}",
                Guild = guild.Name,
                CommandName = command.Name
            };

    }
}
