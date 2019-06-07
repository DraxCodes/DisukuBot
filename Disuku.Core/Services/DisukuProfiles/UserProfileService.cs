using Disuku.Core.Discord;
using Disuku.Core.Entities;
using Disuku.Core.Providers.Profile;
using System.Threading.Tasks;

namespace Disuku.Core.Services.DisukuProfiles
{
    public class UserProfileService
    {
        private readonly IDiscordMessage _discordMessage;
        public UserProfileService(DisukuUserProvider userProvider, IDiscordMessage discordMessage)
        {
            _discordMessage = discordMessage;
        }

        public async Task ReplyUserAsync(ulong chanId, DisukuUser user)
        {
            await _discordMessage.SendDiscordMessageAsync(chanId, user);
        }
    }
}
