using Disuku.Core.Discord;
using Disuku.Core.Entities.Embeds;
using RaiderIO;
using RaiderIO.Entities.Enums;
using System.Text;
using System.Threading.Tasks;

namespace Disuku.Core.Services.RaiderIO
{
    public class RaiderIOService : IRaiderIOService
    {
        private IDiscordMessage _discordMessage;
        public RaiderIOService(IDiscordMessage discordMessage)
        {
            _discordMessage = discordMessage;
        }

        public async Task GetCharacterInfoAsync(ulong chanId, string name, string realm, string region)
        {
            Region definedRegion;

            switch (region.ToLower())
            {
                case "eu":
                    definedRegion = Region.EU;
                    break;
                case "us":
                    definedRegion = Region.US;
                    break;
                case "kr":
                    definedRegion = Region.KR;
                    break;
                case "tw":
                    definedRegion = Region.TW;
                    break;
                default:
                    definedRegion = Region.EU;
                    break;
            }

            var client = new RaiderIOClient(definedRegion, realm, name);
            var characterData = await client.GetCharacterStatsAsync();

            var armoryURL = $"https://worldofwarcraft.com/en-gb/character/{realm}/{name}/";
            var wowanalyzeURL = $"https://www.wowanalyzer.com/character/EU/{realm}/{name}/";

            var embed = new DisukuEmbed
            {
                Title = $"{characterData.Name} {characterData.Realm} | Character Info",
                Description = $"**Name**: {characterData.Name}\n" +
                    $"**Links**: [Raider.IO]({characterData.Url}) | [Armory]({armoryURL}) | [WowAnalzyer]({wowanalyzeURL})\n" +
                    $"**Class**: {characterData.Race}, {characterData.SpecName} {characterData.Class}\n" +
                    $"**Item Level**: Equipped: {characterData.Gear.ItemLevelEquiped} | Overall: {characterData.Gear.ItemLevelAverage}\n" +
                    $"**Raid Progression (Uldir)**: {characterData.GetRaidProgression.Uldir.Summary}\n" +
                    $"**Mythic+**: {characterData.GetMythicPlusScores.Overall}",
                Thumbnail = characterData.Thumbnail
            };

            await _discordMessage.SendDiscordEmbedAsync(chanId, embed);

        }

        public async Task GetAffixesAsync(ulong chanId)
        {
            var affixes = await new RaiderIOClient(Region.EU).GetAffixesAsync(Region.EU);
            var description = new StringBuilder();

            foreach (var affix in affixes.CurrentAffixes)
            {
                description.Append($"**[{affix.Name}]({affix.Url})**\n{affix.Description}\n\n");
            }
            var embed = new DisukuEmbed
            {
                Title = affixes.Title,
                Description = description.ToString(),
                Footer = ("Powered by Raider.IO"),
                Thumbnail = "https://media.forgecdn.net/avatars/117/23/636399071197048271.png"
            };

            await _discordMessage.SendDiscordEmbedAsync(chanId, embed);
        }
    }
}
