﻿using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace Disuku.Discord.Modules
{
    [Name("Misc Commands")]
    public class Misc : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _cmdService;
        public Misc(CommandService cmdService)
        {
            _cmdService = cmdService;
        }

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

        [Command("Roll"), Summary("Picks a number between two set values, default 0-100.")]
        public async Task Roll(int num1 = 0, int num2 = 100)
        {
            var rand = new Random();
            var randNum = rand.Next(num1, num2);
            await ReplyAsync("```diff\n" +
                $"+ {Context.User.Username}: {randNum}\n" +
                "```");
        }
    }
}
