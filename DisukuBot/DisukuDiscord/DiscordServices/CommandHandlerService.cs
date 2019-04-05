using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DisukuBot.DisukuCore.Services.Logger;
using DisukuBot.DisukuDiscord.Converters;
using DisukuBot.DisukuDiscord.Extensions;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace DisukuBot.DisukuDiscord.DiscordServices
{
    public class CommandHandlerService : IServiceExtention
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _cmdService;
        private readonly IServiceProvider _services;
        private readonly IDisukuLogger _logger;

        public CommandHandlerService(IServiceProvider services, DiscordSocketClient client, CommandService cmdService, IDisukuLogger logger)
        {
            _client = client;
            _cmdService = cmdService;
            _services = services;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            await _cmdService.AddModulesAsync(
                Assembly.GetExecutingAssembly(),
                _services);

            HookEvents();
        }

        private void HookEvents()
        {
            _client.MessageReceived += HandlerMessageAsync;
            _cmdService.CommandExecuted += CommandExecutedAsync;
            _cmdService.Log += LogAsync;
        }


        private async Task HandlerMessageAsync(SocketMessage socketMessage)
        {
            //TODO: Add Guild based prefixes.

            if (!(socketMessage is SocketUserMessage message)) return;
            var argPos = 0;

            if (!(message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return;

            var context = new SocketCommandContext(_client, message);

            await _cmdService.ExecuteAsync(
                    context: context,
                    argPos: argPos,
                    services: _services);
        }

        private async Task LogAsync(LogMessage logMessage)
        {
            var disukuLog = DisukuEntityConverter.CovertLog(logMessage);
            await _logger.LogAsync(disukuLog);
        }

        private async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (!command.IsSpecified)
                return;
            var commandLog = DisukuEntityConverter.ConvertCommandLog(context.Guild as SocketGuild, context.Channel as SocketGuildChannel, context.User as SocketGuildUser, command.Value);
            if (result.IsSuccess)
            {
                await _logger.LogCommandAsync(commandLog);
            }
            else
            {
                await _logger.LogCommandAsync(commandLog, result.ErrorReason);
                var embed = new EmbedBuilder()
                    .WithTitle("ERROR")
                    .WithDescription(result.ErrorReason)
                    .WithColor(Color.DarkRed);

                await context.Channel.SendMessageAsync(embed: embed.Build());
            }
        }
    }
}
