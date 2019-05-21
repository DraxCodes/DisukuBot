using System;
using System.Linq;
using System.Threading.Tasks;
using Disuku.Core.Entities;
using Disuku.Core.Storage;

namespace Disuku.Core.Providers.Profile
{
    public class DisukuGuildProvider
    {
        private IMongoDbStorage _mongoDbStorage;

        public DisukuGuildProvider(IMongoDbStorage mongoDbStorage)
        {
            _mongoDbStorage = mongoDbStorage;
            _mongoDbStorage.InitializeDbAsync("DisukuBot");
        }

        public async Task<DisukuGuild> GetGuild(DisukuGuild guild)
        {
            await _mongoDbStorage.UpsertSingleRecordAsync("Guilds", guild.Id, guild);
            var results = await _mongoDbStorage.LoadRecordsAsync<DisukuGuild>("Guilds", u => u.GuildId == guild.GuildId);
            return results.FirstOrDefault();
        }
    }
}
