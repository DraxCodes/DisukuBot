﻿using Disuku.Core.Entities;
using Disuku.Core.Storage;
using System.Linq;
using System.Threading.Tasks;

namespace Disuku.Core.Providers.Profile
{
    public class DisukuGuildProvider
    {
        private readonly IDbStorage _mongoDbStorage;

        public DisukuGuildProvider(IDbStorage mongoDbStorage)
        {
            _mongoDbStorage = mongoDbStorage;
            _mongoDbStorage.InitializeDbAsync("DisukuBot");
        }

        public async Task<DisukuGuild> GetGuild(DisukuGuild guild)
        {
            //TODO: FIX THIS, shouldn't be upserting before getting the info.

            //await _mongoDbStorage.UpsertSingleRecordAsync("Guilds", guild.Id, guild);
            var results = await _mongoDbStorage.LoadRecordsAsync<DisukuGuild>(u => u.GuildId == guild.GuildId, "Guilds");
            return results.FirstOrDefault();
        }
    }
}
