using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DisukuBot.DisukuCore.Entities.Logging;

namespace DisukuBot.DisukuDiscord.Converters
{
    public static class DisukuEntityConverter
    {
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
