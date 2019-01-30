using DisukuBot.DisukuDiscord;
using System;
using System.Threading.Tasks;

namespace DisukuBot
{
    class Program
    {
        static Task Main(string[] args)
            => new DisukuBotClient().InitializeAsync();
    }
}
