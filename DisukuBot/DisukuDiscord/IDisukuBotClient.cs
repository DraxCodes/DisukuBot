using System.Threading.Tasks;

namespace DisukuBot.DisukuDiscord
{
    public interface IDisukuBotClient
    {
        Task InitializeAsync();
    }
}