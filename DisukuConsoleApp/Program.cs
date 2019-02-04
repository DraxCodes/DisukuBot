using DisukuBot.DisukuDiscord;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace DisukuConsoleApp
{
    class Program
    {        
        static async Task Main(string[] args)
        {
            await ActivatorUtilities.CreateInstance<DisukuBotClient>(InversionOfControl.Provider).InitializeAsync();
        }
    }
}
