using Disuku.Core.Discord;
using Disuku.Core.Entities;
using Disuku.Core.Providers.Profile;
using System.Threading.Tasks;

namespace Disuku.Core.Services.DisukuProfiles
{
    public class GuildProfileService
    {
        private readonly DisukuGuildProvider _guildProvider;
        private readonly IDiscordMessage _discordMessage;
        public GuildProfileService(DisukuGuildProvider guildProvider, IDiscordMessage discordMessage)
        {
            _guildProvider = guildProvider;
            _discordMessage = discordMessage;
        }

        public async Task ReplyGuildAsync(ulong chanId, DisukuGuild disukuGuild)
        {
            var guild = await _guildProvider.GetGuild(disukuGuild);
            await _discordMessage.SendDiscordMessageAsync(chanId, guild);
        }
    }
}
