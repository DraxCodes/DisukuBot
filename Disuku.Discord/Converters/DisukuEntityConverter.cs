using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Disuku.Core.Entities;
using Disuku.Core.Entities.Embeds;
using Disuku.Core.Entities.Logging;
using System.Collections.Generic;

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

            if (embed.Author != null && !string.IsNullOrWhiteSpace(embed.Author.Description)) { discordEmbed.WithAuthor($"{embed.Author.Username} <{embed.Author.Description}>", embed.Author.AvatarUrl); }
            if (embed.Author != null && string.IsNullOrWhiteSpace(embed.Author.Description)) { discordEmbed.WithAuthor(embed.Author.Username, embed.Author.AvatarUrl); }
            if (!string.IsNullOrWhiteSpace(embed.Url)) { discordEmbed.WithUrl(embed.Url); }
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
                Severity = ConvertSeverity(logMessage.Severity)
            };

        private static DisukuLogSeverity ConvertSeverity(LogSeverity severity)
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

        public static DisukuUser ConvertToDisukuUser(SocketGuildUser discordUser)
        {
            return new DisukuUser
            {
                UserId = discordUser.Id,
                GuildId = discordUser.Guild.Id,
                Username = discordUser.Nickname ?? discordUser.Username,
                Roles = ConvertToDisukuRoles(discordUser.Roles),
                AvatarUrl = discordUser.GetAvatarUrl(),
                CreatedAt = discordUser.CreatedAt.UtcDateTime,
                JoinedAt = discordUser.JoinedAt.Value.UtcDateTime
            };
        }

        public static DisukuGuild ConvertToDisukuGuild(SocketGuild guild)
        {
            return new DisukuGuild
            {
                GuildId = guild.Id,
                Name = guild.Name,
                TextChannelCount = guild.TextChannels.Count,
                VoiceChannelCount = guild.VoiceChannels.Count,
                CreationDate = guild.CreatedAt.UtcDateTime,
                MemberCount = guild.Users.Count,
                GuildAvatar = guild.IconUrl
            };
        }

        private static IEnumerable<DisukuRole> ConvertToDisukuRoles(IReadOnlyCollection<SocketRole> roles)
        {
            var disukuRoles = new List<DisukuRole>();
            foreach (var role in roles)
            {
                disukuRoles.Add(new DisukuRole
                {
                    Id = role.Id,
                    Name = role.Name
                });
            }
            return disukuRoles;
        }

    }
}
