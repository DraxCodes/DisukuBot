using DisukuBot.DisukuDiscord;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Disuku.Discord
{
    class Program
    {
        static async Task Main(string[] args)
            => await new DisukuBotClient().InitializeAsync();
    }
}
