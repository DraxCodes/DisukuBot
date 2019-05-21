using Discord.Commands;
using Discord.WebSocket;
using Disuku.Core.Services.DisukuProfiles;
using Disuku.Discord.Converters;
using System.Threading.Tasks;

namespace Disuku.Discord.Discord.Modules
{
    [Group("Profile")]
    public class Profile : ModuleBase<SocketCommandContext>
    {
        private UserProfileService _userProfileService;
        private GuildProfileService _guildProfileService;
        public Profile(UserProfileService userProfileService, GuildProfileService guildProfileService)
        {
            _userProfileService = userProfileService;
            _guildProfileService = guildProfileService;
        }

        [Command]
        public async Task DislayUserProfile(SocketGuildUser discordUser = null)
        {
            if (discordUser is null) { discordUser = Context.User as SocketGuildUser; }
            var disukuUser = DisukuEntityConverter.ConvertToDisukuUser(discordUser);
            await _userProfileService.ReplyUserAsync(Context.Channel.Id, disukuUser);
        }

        [Command("Guild")]
        public async Task DisplayGuildProfile()
        {
            var guild = DisukuEntityConverter.ConvertToDisukuGuild(Context.Guild);
            await _guildProfileService.ReplyGuildAsync(Context.Channel.Id, guild);
        }
    }
}
