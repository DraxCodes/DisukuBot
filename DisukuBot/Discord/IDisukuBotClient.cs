using System.Threading.Tasks;

namespace Disuku.Discord
{
    public interface IDisukuBotClient
    {
        Task InitializeAsync();
    }
}