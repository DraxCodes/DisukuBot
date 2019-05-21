using Discord.Commands;
using Discord.WebSocket;
using Disuku.Core.Services.DisukuProfiles;
using Disuku.Discord.Converters;
using System.Threading.Tasks;

namespace Disuku.Discord.Discord.Modules
{
    public class Profile : ModuleBase<SocketCommandContext>
    {
        private UserProfileService _userProfileService;
        public Profile(UserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [Command("Profile")]
        public async Task DislayProfile(SocketGuildUser discordUser = null)
        {
            if (discordUser is null) { discordUser = Context.User as SocketGuildUser; }
            var disukuUser = DisukuEntityConverter.ConvertToDisukuUser(discordUser);
            await _userProfileService.ReplyUserAsync(Context.Channel.Id, disukuUser);
        }
    }
}
