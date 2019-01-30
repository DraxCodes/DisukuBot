using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DisukuBot.DisukuData;
using DisukuBot.DisukuData.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DisukuBot.DisukuDiscord
{
    public class DisukuBotClient
    {
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        private DisukuJsonDataService _dataServices;
        private BotConfig _config;

        public DisukuBotClient(CommandService commands = null, DiscordSocketClient client = null, DisukuJsonDataService dataService = null)
        {
            //Create our new DiscordClient (Setting LogServerity to Verbose)
            _client = client ?? new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose,
                AlwaysDownloadUsers = true,
                MessageCacheSize = 100
            });

            //Create our new CommandService (Setting RunMode to async by default on all commands)
            _commands = commands ?? new CommandService(new CommandServiceConfig
            {
                DefaultRunMode = RunMode.Async,
                CaseSensitiveCommands = false,
            });

            _dataServices = dataService ?? new DisukuJsonDataService();
        }

        public async Task InitializeAsync()
        {
            //Check if Config.Json exists (If not create one)
            if (!_dataServices.Exists(Global.ConfigPath))
                await _dataServices.Save(new BotConfig {
                    Token = "",
                    GameStatus = "Change Me",
                    Prefix = "!bot"
                }, Global.ConfigPath);

            var config = await _dataServices.Retreive<BotConfig>(Global.ConfigPath);

            if (string.IsNullOrWhiteSpace(config.Token))
            {
                Console.WriteLine("Please Enter Your Token: ");
                config.Token = Console.ReadLine();
                await _dataServices.Save(config, Global.ConfigPath);
            }
            _config = config;


            //Setup Our Services
            _services = ConfigureServices();

            //Login with the client
            await _client.LoginAsync(TokenType.Bot, config.Token);

            //Starr the client
            await _client.StartAsync();

            //Hook up our events
            HookEvents();

            //Initialize Our CommandService Handler
            //await _services.GetRequiredService<CommandHandlerService>().InitializeAsync();

            //This is used so the bot doesn't shut down instantly.
            await Task.Delay(-1);
        }

        private void HookEvents()
        {
            _client.Ready += OnReady;
            _client.Log += LogAsync;
        }

        private Task LogAsync(LogMessage arg)
        {
            var sb = new StringBuilder()
                .Append($"Log: {arg.Severity} - {arg.Message}");
            Console.WriteLine($"{sb}");
            return Task.CompletedTask;
        }

        private async Task OnReady()
        {
            await _client.SetGameAsync(_config.GameStatus);
        }

        private IServiceProvider ConfigureServices()
            => new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_commands)
            .AddSingleton<DisukuJsonDataService>()
            .BuildServiceProvider();
    }
}
