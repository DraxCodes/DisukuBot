using DisukuBot.DisukuCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DisukuConsoleApp
{
    class Program
    {        
        static async Task Main(string[] args)
        {
            await ActivatorUtilities.CreateInstance<BotClient>(InversionOfControl.Provider).RunAsync();
        }
    }
}
