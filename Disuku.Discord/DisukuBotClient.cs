using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Disuku.Core.Discord;
using Disuku.Core.Entities.Logging;
using Disuku.Core.Services.Logger;
using Disuku.Discord.Converters;
using Disuku.Discord.DisukuDiscord.Extensions;
using Disuku.Discord.JsonData;
using Disuku.Discord.JsonData.Entities;
using Disuku.InversionOfControl;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Disuku.Discord.Adapters;
using Disuku.Discord.DiscordServices;
using Disuku.Discord.Events;

namespace Disuku.Discord
{
    public class DisukuBotClient : IDisukuBotClient
    {
        private DiscordSocketClient _client;
        private IServiceProvider _services;


        private IDisukuLogger _logger;
        private BotConfig _config;

        private readonly DiscordSocketConfig _discordConfigOptions = new DiscordSocketConfig
        {
            LogLevel = LogSeverity.Info,
            AlwaysDownloadUsers = true,
            MessageCacheSize = 50
        };

        private readonly CommandServiceConfig _commandServiceConfigOptions = new CommandServiceConfig
        {
            DefaultRunMode = RunMode.Async,
            CaseSensitiveCommands = false,
        };

        public async Task InitializeAsync()
        {
            _services = ConfigureDiscordServices();
            _config = await InitializeConfigAsync();
            await _services.InitializeServicesAsync();

            _client = _services.GetRequiredService<DiscordSocketClient>();
            _logger = _services.GetRequiredService<IDisukuLogger>();

            await _logger.InitializeConsoleHeaderAsync();
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
            _client.ReactionAdded += _client_ReactionAdded;
        }

        private async Task _client_ReactionAdded(Cacheable<IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
        {
            await arg1.GetOrDownloadAsync();
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
            await _client.SetGameAsync(_config.GameStatus, "", ActivityType.Watching);
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

        private IServiceProvider ConfigureDiscordServices()
            => new ServiceCollection()
                .AddSingleton(new DiscordSocketClient(_discordConfigOptions))
                .AddSingleton(new CommandService(_commandServiceConfigOptions))
                .AddSingleton<CommandHandlerService>()
                .AddTransient<IDiscordMessage, DiscordMessage>()
                .AddSingleton<IDiscordEvents, DiscordEvents>()
                .AddDisukuTypes()
                .BuildServiceProvider();

    }
}
