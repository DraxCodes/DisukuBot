using System.Threading.Tasks;

namespace DisukuBot.DisukuCore
{
    public class BotClient
    {
        private readonly IDisukuBotClient _disukuBotClient;

        public BotClient(IDisukuBotClient disukuBotClient)
        {
            _disukuBotClient = disukuBotClient;
        }

        public async Task RunAsync()
        {
            await _disukuBotClient.InitializeAsync();
        }
    }
}
