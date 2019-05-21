using Disuku.Core.Entities;
using Disuku.Core.Entities.Embeds;
using System.Threading.Tasks;

namespace Disuku.Core.Discord
{
    public interface IDiscordMessage
    {
        Task SendDiscordMessageAsync(ulong chanId, string message);
        Task SendDiscordMessageAsync(ulong chanId, DisukuUser user);
        Task SendDiscordMessageAsync(ulong chanId, DisukuGuild guild);
        Task SendDiscordEmbedAsync(ulong chanId, DisukuEmbed embed);
    }
}
