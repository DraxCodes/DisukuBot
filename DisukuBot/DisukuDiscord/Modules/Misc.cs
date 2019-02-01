using Discord;
using Discord.Commands;
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
            string[] collection = {  };
            var rand = new Random();
            var index = rand.Next(collection.Length);

            await ReplyAsync("");
        }
    }
}
