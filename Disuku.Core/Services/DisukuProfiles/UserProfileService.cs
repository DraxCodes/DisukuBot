using Disuku.Core.Discord;
using Disuku.Core.Entities;
using System.Threading.Tasks;

namespace Disuku.Core.Services.DisukuProfiles
{
    public class UserProfileService
    {
        private readonly IDiscordMessage _discordMessage;
        public UserProfileService(IDiscordMessage discordMessage)
        {
            _discordMessage = discordMessage;
        }

        public async Task ReplyUserAsync(ulong chanId, DisukuUser user)
        {
            await _discordMessage.SendDiscordMessageAsync(chanId, user);
        }
    }
}
