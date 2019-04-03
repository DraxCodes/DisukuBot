using DisukuBot.DisukuCore.Entities.Logging;
using System;
using System.Threading.Tasks;

namespace DisukuBot.DisukuCore.Services.Logger
{
    public class DisukuLogger : IDisukuLogger
    {                                                                                                                                                                            
        public async Task LogAsync(DisukuLog logMessage)
        {
            await Append($"{ConvertSource(logMessage.Source)} ", ConsoleColor.DarkGray);
            await Append($"[{logMessage.Severity}] ", await SeverityColor(logMessage.Severity));
            await Append($"{logMessage.Message}\n", ConsoleColor.White);
        }

        public async Task LogCriticalAsync(DisukuLog logMessage, Exception exception)
        {
            await Append($"{ConvertSource(logMessage.Source)} ", ConsoleColor.DarkGray);
            await Append($"[{logMessage.Severity}] ", await SeverityColor(logMessage.Severity));
            await Append($"{logMessage.Message}\n", ConsoleColor.White);
            await Append($"{exception.Message}", ConsoleColor.DarkGray);
        }

        public async Task LogCommandAsync(DisukuCommandLog log)
        {
            await Append("DISC ", ConsoleColor.DarkGray);
            await Append("[CMND] ", ConsoleColor.Magenta);
            await Append($"Command {log.CommandName} Executed For {log.User} in {log.Guild}/#{log.Channel}\n", ConsoleColor.White);
        }

        public async Task LogCommandAsync(DisukuCommandLog log, string error)
        {
            await Append("DISC ", ConsoleColor.DarkGray);
            await Append("[CMND] ", ConsoleColor.Magenta);
            await Append($"Command ERROR: {error} For {log.User} in {log.Guild}/#{log.Channel}\n", ConsoleColor.White);
        }

        private async Task Append(string message, ConsoleColor color)
        {
            await Task.Run(() => {
                Console.ForegroundColor = color;
                Console.Write(message);
                return Task.CompletedTask;
            });
        }

        private string ConvertSource(string source)
        {
            switch (source.ToLower())
            {
                case "discord":
                    return "DISC";
                case "gateway":
                    return "GTWY";
                case "command":
                    return "COMD";
                case "rest":
                    return "REST";
                default:
                    return source;
            }
        }

        private Task<ConsoleColor> SeverityColor(DisukuLogSeverity severity)
        {
            switch (severity)
            {
                case DisukuLogSeverity.Critical:
                    return Task.FromResult(ConsoleColor.Red);
                case DisukuLogSeverity.Error:
                    return Task.FromResult(ConsoleColor.DarkRed);
                case DisukuLogSeverity.Warning:
                    return Task.FromResult(ConsoleColor.Yellow);
                case DisukuLogSeverity.Info:
                    return Task.FromResult(ConsoleColor.Green);
                default:
                    return Task.FromResult(ConsoleColor.DarkGray);
            }
        }

    }
}
