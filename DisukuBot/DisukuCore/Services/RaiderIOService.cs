using DisukuBot.DisukuDiscord.Extensions;
using RaiderIO;
using RaiderIO.Entities.Enums;
using System.Threading.Tasks;

namespace DisukuBot.DisukuCore.Services
{
    public class RaiderIOService : IServiceExtention
    {
        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public async Task<string> GetInfo(string name, string realm)
        {
            var client = new RaiderIOClient(Region.EU, realm, name);
            var characterData = await client.GetCharacterStatsAsync();
            var armoryURL = $"https://worldofwarcraft.com/en-gb/character/{realm}/{name}/";
            var wowanalyzeURL = $"https://www.wowanalyzer.com/character/EU/{realm}/{name}/";

            return characterData.Class;
        }
    }
}
