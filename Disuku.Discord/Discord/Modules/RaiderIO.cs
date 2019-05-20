using Discord;
using Discord.Commands;
using Disuku.Core.Services.RaiderIO;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Disuku.Discord.Modules
{
    public class RaiderIO : ModuleBase<SocketCommandContext>
    {
        private IRaiderIOService _raiderIOService;

        public RaiderIO(IRaiderIOService raiderIOService)
        {
            _raiderIOService = raiderIOService;
        }

        [Command("Affix"), Name("Affix")]
        [Summary("Gets the currents week affixes.")]
        public async Task Affixes()
        {
            //var affixes = await _raiderIOService.GetAffixesAsync();

            //var details = new StringBuilder();

            //var embed = new EmbedBuilder().
            //    WithTitle(affixes.Title)
            //    .WithThumbnailUrl("https://media.forgecdn.net/avatars/117/23/636399071197048271.png")
            //    .WithFooter("Powered by Raider.IO", "https://media.forgecdn.net/avatars/117/23/636399071197048271.png")
            //    .WithTimestamp(DateTime.UtcNow)
            //    .WithColor(Color.DarkRed);

            //foreach (var affix in affixes.Segments)
            //{
            //    details.Append($"**[{affix.Title}]({affix.Url})**\n{affix.Description}\n\n");
            //}
            //embed.WithDescription(details.ToString());

            //await ReplyAsync(embed: embed.Build());
        }

        [Command("Stats"), Name("Stats")]
        [Summary("Gets the World Of Warcraft Stats for the specified user.")]
        public async Task Stats(string name, string realm, string region = "eu")
            => await _raiderIOService.GetCharacterInfoAsync(Context.Channel.Id, name, realm, region);
    }
}
