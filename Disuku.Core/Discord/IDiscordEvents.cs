using System;

namespace Disuku.Core.Discord
{
    public interface IDiscordEvents
    {
        event EventHandler ClientReady;
    }
}
