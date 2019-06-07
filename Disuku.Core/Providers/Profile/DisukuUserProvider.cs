using Disuku.Core.Entities;
using Disuku.Core.Storage;
using System.Linq;
using System.Threading.Tasks;

namespace Disuku.Core.Providers.Profile
{
    public class DisukuUserProvider
    {
        private readonly IDbStorage _mongoDbStorage;

        public DisukuUserProvider(IDbStorage mongoDbStorage)
        {
            _mongoDbStorage = mongoDbStorage;
            _mongoDbStorage.InitializeDbAsync("DisukuBot");
        }

        public async Task<DisukuUser> GetUser(DisukuUser disukuUser)
        {
            await _mongoDbStorage.UpsertSingleRecordAsync("Users", disukuUser.Id, disukuUser);
            var users = await _mongoDbStorage.LoadRecordsAsync<DisukuUser>("Users", u => u.UserId == disukuUser.UserId && u.GuildId == disukuUser.GuildId);
            return users.FirstOrDefault();
        }

    }
}
