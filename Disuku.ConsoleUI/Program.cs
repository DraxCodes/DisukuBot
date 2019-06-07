using System;
using System.Threading.Tasks;

namespace Disuku.ConsoleUI
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Hello World!");
            });
        }
    }
}
