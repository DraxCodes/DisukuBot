using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DisukuBot.DisukuDiscord.DiscordServices;
using DisukuJsonData;
using DisukuJsonData.Entities;
using DisukuBot.DisukuDiscord.Converters;
using DisukuJsonData.Interfaces;
using Disuku.Core.Services.Logger;
using Disuku.Discord.DisukuDiscord.Extensions;

namespace DisukuBot.DisukuDiscord
{
    public class DisukuBotClient : IDisukuBotClient
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private IServiceProvider _services;
        private readonly IDisukuJsonDataService _dataServices;
        private readonly IDisukuLogger _logger;
        private BotConfig _config;

        public DisukuBotClient(CommandService commands = null, DiscordSocketClient client = null, IDisukuJsonDataService dataService = null, IDisukuLogger logger = null)
        {
            _client = client ?? new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug,
                AlwaysDownloadUsers = true,
                MessageCacheSize = 100
            });

            _commands = commands ?? new CommandService(new CommandServiceConfig
            {
                DefaultRunMode = RunMode.Async,
                CaseSensitiveCommands = false,
            });

            _dataServices = dataService ?? new DisukuJsonDataService();

            _logger = logger ?? new DisukuLogger();

        }

        public async Task InitializeAsync()
        {
            _config = await InitializeConfigAsync();
            _services = ConfigureServices();
            await _services.InitializeServicesAsync();

            await _client.LoginAsync(TokenType.Bot, _config.Token);
            await _client.StartAsync();

            HookEvents();
            await Task.Delay(-1);
        }
        
        private async Task<BotConfig> InitializeConfigAsync()
        {
            if (!_dataServices.FileExists(Global.ConfigPath))
                await _dataServices.Save(new BotConfig
                {
                    Token = "",
                    GameStatus = "Change Me",
                    Prefix = "bot!"
                }, Global.ConfigPath);

            var config = await _dataServices.Retreive<BotConfig>(Global.ConfigPath);

            if (string.IsNullOrWhiteSpace(config.Token))
            {
                Console.WriteLine("Please Enter Your Token: ");
                config.Token = Console.ReadLine();
                await _dataServices.Save(config, Global.ConfigPath);
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

            if (logMessage.Exception == null)
                await _logger.LogAsync(disukuLog);
            else
                await _logger.LogCriticalAsync(disukuLog, logMessage.Exception);
        }


        private async Task OnReady()
        {
            await _client.SetGameAsync(_config.GameStatus);
        }

        private IServiceProvider ConfigureServices()
            => new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_commands)
            .AddSingleton(_dataServices)
            .AddSingleton(_logger)
            .AddSingleton<CommandHandlerService>()
            .AddDisukuTypes()
            .AutoAddServices()
            .BuildServiceProvider();
    }
}
