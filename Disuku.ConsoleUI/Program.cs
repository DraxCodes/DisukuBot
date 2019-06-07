using System.Threading.Tasks;
using Disuku.Discord;

namespace Disuku.ConsoleUI
{
    internal class Program
    {
        private static async Task Main(string[] args)
            => await new DisukuBotClient().InitializeAsync();
    }
}
