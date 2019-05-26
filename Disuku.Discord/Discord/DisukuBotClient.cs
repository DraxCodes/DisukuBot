using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Disuku.Core.Discord;
using Disuku.Discord.JsonData;
using Disuku.Discord.JsonData.Entities;
using Disuku.Discord.DisukuDiscord.Extensions;
using Disuku.Core.Services.Logger;
using Disuku.Discord.DisordServices;
using Disuku.Discord.Converters;
using Disuku.Core.Entities.Logging;

namespace Disuku.Discord
{
    public class DisukuBotClient : IDisukuBotClient
    {
        private DiscordSocketClient _client;
        private IServiceProvider _services;


        private IDisukuLogger _logger;
        private BotConfig _config;

        private DiscordSocketConfig _discordConfigOptions = new DiscordSocketConfig
        {
            LogLevel = LogSeverity.Info,
            AlwaysDownloadUsers = true,
            MessageCacheSize = 50
        };

        private CommandServiceConfig _commandServiceConfigOptions = new CommandServiceConfig
        {
            DefaultRunMode = RunMode.Async,
            CaseSensitiveCommands = false,
        };

        public async Task InitializeAsync()
        {
            _config = await InitializeConfigAsync();
            _services = ConfigureServices();
            await _services.InitializeServicesAsync();

            _client = _services.GetRequiredService<DiscordSocketClient>();
            _logger = _services.GetRequiredService<IDisukuLogger>();

            await _logger.InitializeHeaderAsync();
            await _client.LoginAsync(TokenType.Bot, _config.Token);
            await _client.StartAsync();
            HookEvents();
            await Task.Delay(-1);
        }

        private async Task<BotConfig> InitializeConfigAsync()
        {
            var jsonServices = new DisukuJsonDataService();

            if (!jsonServices.FileExists(Global.ConfigPath))
                await jsonServices.Save(new BotConfig
                {
                    Token = "",
                    GameStatus = "Change Me",
                    Prefix = "bot!"
                }, Global.ConfigPath);

            var config = await jsonServices.Retreive<BotConfig>(Global.ConfigPath);

            if (string.IsNullOrWhiteSpace(config.Token))
            {
                Console.WriteLine("Please Enter Your Token: ");
                config.Token = Console.ReadLine();
                await jsonServices.Save(config, Global.ConfigPath);
            }
            return config;
        }

        private void HookEvents()
        {
            _client.Ready += OnReady;
            _client.Log += LogAsync;
        }

        private async Task LogAsync(LogMessage logMessage)
        {
            var disukuLog = DisukuEntityConverter.CovertLog(logMessage);

            if (logMessage.Exception is null)
                await _logger.LogAsync(disukuLog);
            else
                await _logger.LogCriticalAsync(disukuLog, logMessage.Exception);
        }


        private async Task OnReady()
        {
            await _client.SetGameAsync(_config.GameStatus);
            foreach (var guild in _client.Guilds)
            {
                var log = new DisukuLog
                {
                    Message = $"Active in: {guild.Name}",
                    Severity = DisukuLogSeverity.Info,
                    Source = "Discord"
                };
                await _logger.LogAsync(log);
            }
        }

        private IServiceProvider ConfigureServices()
            => new ServiceCollection()
                .AddSingleton(new DiscordSocketClient(_discordConfigOptions))
                .AddSingleton(new CommandService(_commandServiceConfigOptions))
                .AddSingleton<CommandHandlerService>()
                .AddDisukuTypes()
                .BuildServiceProvider();

    }
}
