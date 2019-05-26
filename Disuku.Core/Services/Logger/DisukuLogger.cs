using Disuku.Core.Entities.Logging;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Console = Colorful.Console;
using Color = System.Drawing.Color;

namespace Disuku.Core.Services.Logger
{
    public class DisukuLogger : IDisukuLogger
    {
        public Task InitializeHeaderAsync()
        {
            const string header = @"
            █▀▀▄ ░▀░ █▀▀ █░░█ █░█ █░░█ █▀▀▄ █▀▀█ ▀▀█▀▀
            █░░█ ▀█▀ ▀▀█ █░░█ █▀▄ █░░█ █▀▀▄ █░░█ ░░█░░
            ▀▀▀░ ▀▀▀ ▀▀▀ ░▀▀▀ ▀░▀ ░▀▀▀ ▀▀▀░ ▀▀▀▀ ░░▀░░";
            var lineBreak = $"\n{new string('-', 90)}\n";
            var process = Process.GetCurrentProcess();

            Console.WriteLine(header, Color.Teal);
            Console.WriteLine(lineBreak, Color.LightCoral);
            Console.Write("     Runtime: ", Color.Plum);
            Console.Write($"{RuntimeInformation.FrameworkDescription}\n");
            Console.Write("     Process: ", Color.Plum);
            Console.Write($"{process.Id} ID | {process.Threads.Count} Threads\n");
            Console.Write("          OS: ", Color.Plum);
            Console.Write($"{RuntimeInformation.OSDescription} | {RuntimeInformation.ProcessArchitecture}\n");
            Console.WriteLine(lineBreak, Color.LightCoral);
            return Task.CompletedTask;
        }

        public async Task LogAsync(DisukuLog logMessage)
        {
            var date = $"[{DateTimeOffset.Now:MMM d - hh:mm:ss tt}]";
            Append($"{date} ", Color.DarkGray);
            Append($"[{logMessage.Severity}] ", await SeverityColor(logMessage.Severity));
            Append($"{ConvertSource(logMessage.Source)} ", Color.DarkGray);
            Append($"{logMessage.Message}\n", Color.White);
        }

        public async Task LogCriticalAsync(DisukuLog logMessage, Exception exception)
        {
            var date = $"[{DateTimeOffset.Now:MMM d - hh:mm:ss tt}]";
            Append($"{date} ", Color.DarkGray);
            Append($"{ConvertSource(logMessage.Source)} ", Color.DarkGray);
            Append($"[{logMessage.Severity}] ", await SeverityColor(logMessage.Severity));
            Append($"{logMessage.Message}", Color.White);
            Append($"{exception.Message}\n", Color.DarkGray);
        }

        public Task LogCommandAsync(DisukuCommandLog log)
        {
            var date = $"[{DateTimeOffset.Now:MMM d - hh:mm:ss tt}]";
            Append($"{date} ", Color.DarkGray);
            Append("[Info] ", Color.LightGreen);
            Append("Command ", Color.LightGray);
            Append($"{log.CommandName} Executed For {log.User} in {log.Guild}/#{log.Channel}\n", Color.White);
            return Task.CompletedTask;
        }

        public Task LogCommandAsync(DisukuCommandLog log, string error)
        {
            var date = $"[{DateTimeOffset.Now:MMM d - hh:mm:ss tt}]";
            Append($"{date} ", Color.DarkGray);
            Append("[CMND] ", Color.LightCyan);
            Append("Discord", Color.LightGray);
            Append($"Command ERROR: {error} For {log.User} in {log.Guild}/#{log.Channel}\n", Color.White);
            return Task.CompletedTask;
        }

        private void Append(string message, Color color)
        {
            if (Console.CursorTop > 25)
            {
                ClearLowerConsoleLines();
            }
            Console.Write($"{message}", color);
        }

        private void ClearLowerConsoleLines()
        {
            for (int i = 12; i < 26; i++)
            {
                Console.SetCursorPosition(0, i);
                ResetConsoleLine(i);
            }
            Console.SetCursorPosition(0, 12);
        }

        private void ResetConsoleLine(int pos)
        {
            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write(" ");
            Console.SetCursorPosition(0, pos);
        }

        private string ConvertSource(string source)
        {
            switch (source.ToLower())
            {
                case "discord":
                    return "Discord";
                case "gateway":
                    return "Gateway";
                case "command":
                    return "Command";
                case "rest":
                    return "RestSer";
                default:
                    return source;
            }
        }

        private Task<Color> SeverityColor(DisukuLogSeverity severity)
        {
            switch (severity)
            {
                case DisukuLogSeverity.Critical:
                    return Task.FromResult(Color.Red);
                case DisukuLogSeverity.Error:
                    return Task.FromResult(Color.DarkRed);
                case DisukuLogSeverity.Warning:
                    return Task.FromResult(Color.Yellow);
                case DisukuLogSeverity.Info:
                    return Task.FromResult(Color.LightGreen);
                default:
                    return Task.FromResult(Color.Lime);
            }
        }

    }
}
