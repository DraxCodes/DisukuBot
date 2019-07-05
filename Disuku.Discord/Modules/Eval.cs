using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace Disuku.Discord.Modules
{
    [RequireOwner]
    public class Eval : ModuleBase<SocketCommandContext>
    {
        [Command("Eval")]
        public async Task Evaluate([Remainder] string code)
        {
            
        }
    }
}
