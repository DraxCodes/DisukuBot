using DisukuBot.DisukuCore.Entities.RaiderIO;
using RaiderIO;
using RaiderIO.Entities;
using RaiderIO.Entities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DisukuBot.DisukuCore.Services.RaiderIO
{
    public class RaiderIOService : IRaiderIOService
    {
        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public async Task<CharacterExtended> GetCharacterInfoAsync(string name, string realm, string region)
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
            return characterData;
        }

        public async Task<Affix> GetAffixesAsync()
        {
            var affixes = await new RaiderIOClient(Region.EU).GetAffixesAsync(Region.EU);
            var result = new Affix
            {
                Title = $"Region: {affixes.Region.ToUpper()} - {affixes.Title}",
                Segments = new List<Segment>()
            };

            foreach (var item in affixes.CurrentAffixes)
            {
                result.Segments.Add(new Segment
                {
                    Title = item.Name,
                    Description = item.Description,
                    Url = item.Url
                });
            }

            return result;
        }
    }
}
