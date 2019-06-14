using Disuku.Core.Entities;
using Disuku.Core.Storage;
using System.Linq;
using System.Threading.Tasks;

namespace Disuku.Core.Providers.Profile
{
    public class DisukuGuildProvider
    {
        private readonly IPersistentStorage _mongoPersistentStorage;

        public DisukuGuildProvider(IPersistentStorage mongoPersistentStorage)
        {
            _mongoPersistentStorage = mongoPersistentStorage;
            _mongoPersistentStorage.InitializeDbAsync("DisukuBot");
        }

        public async Task<DisukuGuild> GetGuild(DisukuGuild guild)
        {
            var results = await _mongoPersistentStorage.LoadRecordsAsync<DisukuGuild>(u => u.GuildId == guild.GuildId, "Guilds");
            return results.FirstOrDefault();
        }
    }
}
