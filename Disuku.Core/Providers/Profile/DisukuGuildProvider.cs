using Disuku.Core.Entities;
using Disuku.Core.Storage;
using System.Linq;
using System.Threading.Tasks;

namespace Disuku.Core.Providers.Profile
{
    public class DisukuGuildProvider
    {
        private readonly IDataStore _mongoDataStore;

        public DisukuGuildProvider(IDataStore mongoDataStore)
        {
            _mongoDataStore = mongoDataStore;
            _mongoDataStore.InitializeDbAsync("DisukuBot");
        }

        public async Task<DisukuGuild> GetGuild(DisukuGuild guild)
        {
            var results = await _mongoDataStore.LoadRecordsAsync<DisukuGuild>(u => u.GuildId == guild.GuildId, "Guilds");
            return results.FirstOrDefault();
        }
    }
}
