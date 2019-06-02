using System;
using System.Threading.Tasks;

namespace Disuku.ConsoleUI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Hello World!");
            });
        }
    }
}
