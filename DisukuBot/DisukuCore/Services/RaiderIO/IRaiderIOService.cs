using DisukuBot.DisukuCore.Entities.RaiderIO;
using DisukuBot.DisukuDiscord.Extensions;
using RaiderIO.Entities;
using System.Threading.Tasks;

namespace DisukuBot.DisukuCore.Services.RaiderIO
{
    public interface IRaiderIOService
    {
        Task<CharacterExtended> GetCharacterInfoAsync(string name, string realm, string region);
        Task<Affix> GetAffixesAsync();
    }
}
