using Disuku.Core.Entities.Embeds;
using System.Threading.Tasks;

namespace Disuku.Core.Discord
{
    public interface IDiscordMessage
    {
        Task SendDiscordMessageAsync(ulong chanId, string message);
        Task SendDiscordEmbedAsync(ulong chanId, DisukuEmbed embed);
    }
}
