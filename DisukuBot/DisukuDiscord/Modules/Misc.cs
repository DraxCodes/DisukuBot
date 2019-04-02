using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DisukuBot.DisukuDiscord.Modules
{
    [Name("Misc Commands")]
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("Choose"), Summary("Selects between options given split by a comma.")]
        public async Task Choose([Remainder]string message)
        {
            if (!message.Contains(','))
            {
                await ReplyAsync("You don't seem to have given me enough options.");
                return;
            }
            var options = message.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var rand = new Random();
            var randNum = rand.Next(0, options.Length);
            await ReplyAsync($"Hmm, I choose: `{options[randNum]}`");
        }
    }
}
