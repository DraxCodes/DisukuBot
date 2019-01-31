using DisukuBot.DisukuCore.Entities.RaiderIO;
using System.Threading.Tasks;

namespace DisukuBot.DisukuCore.Interfaces
{
    public interface IWorldOfWarcraftService
    {
        Task<RaiderIOInfo> GetPlayerStats(string character, string realm);
    }
}
