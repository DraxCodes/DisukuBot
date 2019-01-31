using System.Threading.Tasks;
using DisukuBot.DisukuCore.Entities.RaiderIO;
using DisukuBot.DisukuDiscord.Extensions;
using RaiderIO;
using RaiderIO.Entities.Enums;

namespace DisukuBot.DisukuCore.Services
{
    public class WorldOfWarcraftService : IServiceExtention
    {
        public Task InitializeAsync()
            => Task.CompletedTask;

        public async Task<RaiderIOInfo> GetPlayerStats(string character, string realm)
        {
            var client = new RaiderIOClient(Region.EU, realm, character);
            var characterData = await client.GetCharacterStatsAsync();
            var armoryURL = $"https://worldofwarcraft.com/en-gb/character/{realm}/{character}/";
            var wowanalyzeURL = $"https://www.wowanalyzer.com/character/EU/{realm}/{character}/";

            return new RaiderIOInfo
            {
                Title = $"{characterData.Name} {characterData.Realm} EU | Character Info",
                Description = $"**Name**: { characterData.Name}\n" +
                $"**Links**: [Raider.IO]({characterData.Url}) | [Armory]({armoryURL}) | [WowAnalzyer]({wowanalyzeURL})\n" +
                $"**Class**: {characterData.Race}, {characterData.SpecName} {characterData.Class}\n" +
                $"**Item Level**: Equipped: {characterData.Gear.ItemLevelEquiped} | Overall: {characterData.Gear.ItemLevelAverage}\n" +
                $"**Raid Progression (Uldir)**: {characterData.GetRaidProgression.Uldir.Summary}\n" +
                $"**Mythic+**: {characterData.GetMythicPlusScores.Overall}",
                ThumbnailUrl = characterData.Thumbnail,
                Faction = characterData.Faction
            };
        } 
    }
}
