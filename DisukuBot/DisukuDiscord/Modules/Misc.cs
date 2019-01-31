using Discord;
using Discord.Commands;
using DisukuBot.DisukuCore.Services;
using System.Threading.Tasks;

namespace DisukuBot.DisukuDiscord.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        private WorldOfWarcraftService _wowService;

        public Misc(WorldOfWarcraftService wowService)
        {
            _wowService = wowService;
        }

        [Command("Echo")]
        public async Task Echo([Remainder]string text)
        {
            await ReplyAsync(text);
        }

        [Command("stats")]
        public async Task RaiderStats(string character, string realm)
        {
            var stats = await _wowService.GetPlayerStats(character, realm);
            var embed = new EmbedBuilder()
                .WithTitle(stats.Title)
                .WithThumbnailUrl(stats.ThumbnailUrl)
                .WithDescription(stats.Description);
            if (stats.Faction.ToLower() == "horde")
                embed.Color = Color.DarkRed;
            else
                embed.Color = Color.DarkBlue;
            await ReplyAsync("", false, embed.Build());
        }
    }
}
