using System.Threading.Tasks;

namespace Disuku.Core.Discord
{
    public interface IDisukuBotClient
    {
        Task InitializeAsync();
    }
}
