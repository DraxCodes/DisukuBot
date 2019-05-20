using System.Threading.Tasks;

namespace Disuku.Core.Services.RaiderIO
{
    public interface IRaiderIOService
    {
        Task GetCharacterInfoAsync(ulong chanId, string name, string realm, string region);
        Task GetAffixesAsync(ulong chanId);
    }
}
