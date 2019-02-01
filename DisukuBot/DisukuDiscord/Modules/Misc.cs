using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using DisukuBot.DisukuCore.Services;
using System;
using System.Threading.Tasks;

namespace DisukuBot.DisukuDiscord.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("Test")]
        public async Task Echo()
        {
            var pins = await Context.Channel.GetPinnedMessagesAsync();
            if (pins.Count > 49)
            {
                foreach (var pin in pins)
                {
                    await ((RestUserMessage)pin).UnpinAsync();
                    
                }
            }
        }
    }
}
