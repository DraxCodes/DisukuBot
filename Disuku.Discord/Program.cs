using System.Threading.Tasks;

namespace Disuku.Discord
{
    class Program
    {
        static async Task Main()
            => await new DisukuBotClient().InitializeAsync();
    }
}
