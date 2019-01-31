using Discord;
using DisukuBot.DisukuCore.Entities.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DisukuBot.DisukuDiscord.Converters
{
    public static class DisukuEntityConverter
    {
        public static DisukuLogMessage CovertLog(LogMessage logMessage)
            => new DisukuLogMessage
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
    }
}
