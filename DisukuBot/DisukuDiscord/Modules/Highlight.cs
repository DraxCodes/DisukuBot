using Discord.Commands;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DisukuBot.DisukuDiscord.Modules
{
    public class Highlight : ModuleBase<SocketCommandContext>
    {
        [Command("Remove")]
        public async Task RemoveHighlight()
        {
            await ReplyAsync("Not implemented yet.");
        }
    }
}
