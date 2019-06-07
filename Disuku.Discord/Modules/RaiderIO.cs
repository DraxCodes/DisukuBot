using Discord.Commands;
using Disuku.Core.Services.RaiderIO;
using System.Threading.Tasks;

namespace Disuku.Discord.Modules
{
    public class RaiderIO : ModuleBase<SocketCommandContext>
    {
        private readonly IRaiderIOService _raiderIOService;

        public RaiderIO(IRaiderIOService raiderIOService)
        {
            _raiderIOService = raiderIOService;
        }

        [Command("Affix"), Name("Affix")]
        [Summary("Gets the currents week affixes.")]
        public async Task Affixes()
            => await _raiderIOService.GetAffixesAsync(Context.Channel.Id);

        [Command("Stats"), Name("Stats")]
        [Summary("Gets the World Of Warcraft Stats for the specified user.")]
        public async Task Stats(string name, string realm, string region = "eu")
            => await _raiderIOService.GetCharacterInfoAsync(Context.Channel.Id, name, realm, region);
    }
}
