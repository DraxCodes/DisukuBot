using Disuku.Core.Discord;
using Disuku.Core.Entities;
using Disuku.Core.Providers.Profile;
using System.Threading.Tasks;

namespace Disuku.Core.Services.DisukuProfiles
{
    public class UserProfileService
    {
        private DisukuUserProvider _userProvider;
        private IDiscordMessage _discordMessage;
        public UserProfileService(DisukuUserProvider userProvider, IDiscordMessage discordMessage)
        {
            _userProvider = userProvider;
            _discordMessage = discordMessage;
        }

        public async Task ReplyUserAsync(ulong chanId, DisukuUser user)
        {
            var disukuUser = await _userProvider.GetUser(user);
            await _discordMessage.SendDiscordMessageAsync(chanId, disukuUser.Id.ToString());
        }
    }
}
