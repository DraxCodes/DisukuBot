using Discord;
using Discord.Commands;
using DisukuBot.DisukuCore.Services.RaiderIO;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DisukuBot.DisukuDiscord.Modules
{
    public class RaiderIO : ModuleBase<SocketCommandContext>
    {
        private RaiderIOService _raiderIOService;

        public RaiderIO(RaiderIOService raiderIOService)
        {
            _raiderIOService = raiderIOService;
        }

        [Command("Affix"), Name("Affix")]
        [Summary("Gets the currents week affixes.")]
        public async Task Affixes()
        {
            var affixes = await _raiderIOService.GetAffixesAsync();

            var details = new StringBuilder();

            var embed = new EmbedBuilder().
                WithTitle(affixes.Title)
                .WithThumbnailUrl("https://media.forgecdn.net/avatars/117/23/636399071197048271.png")
                .WithFooter("Powered by Raider.IO", "https://media.forgecdn.net/avatars/117/23/636399071197048271.png")
                .WithTimestamp(DateTime.UtcNow)
                .WithColor(Color.DarkRed);

            foreach (var affix in affixes.Segments)
            {
                details.Append($"**[{affix.Title}]({affix.Url})**\n{affix.Description}\n\n");
            }
            embed.WithDescription(details.ToString());

            await ReplyAsync(embed: embed.Build());
        }

        [Command("Stats"), Name("Stats")]
        [Summary("Gets the World Of Warcraft Stats for the specified user.")]
        public async Task Stats(string name, string realm, string region = "eu")
        {
            var characterData = await _raiderIOService.GetCharacterInfoAsync(name, realm, region);
            var armoryURL = $"https://worldofwarcraft.com/en-gb/character/{realm}/{name}/";
            var wowanalyzeURL = $"https://www.wowanalyzer.com/character/EU/{realm}/{name}/";

            var embed = new EmbedBuilder
            {
                Title = $"{characterData.Name} {characterData.Realm} EU | Character Info",
                Description = $"**Name**: {characterData.Name}\n" +
                    $"**Links**: [Raider.IO]({characterData.Url}) | [Armory]({armoryURL}) | [WowAnalzyer]({wowanalyzeURL})\n" +
                    $"**Class**: {characterData.Race}, {characterData.SpecName} {characterData.Class}\n" +
                    $"**Item Level**: Equipped: {characterData.Gear.ItemLevelEquiped} | Overall: {characterData.Gear.ItemLevelAverage}\n" +
                    $"**Raid Progression (Uldir)**: {characterData.GetRaidProgression.Uldir.Summary}\n" +
                    $"**Mythic+**: {characterData.GetMythicPlusScores.Overall}",
                ThumbnailUrl = characterData.Thumbnail
            };
            if (characterData.Faction.ToLower() == "horde") { embed.Color = Color.DarkRed; } else { embed.Color = Color.DarkBlue; }

            await ReplyAsync(embed: embed.Build());
        }
    }
}
