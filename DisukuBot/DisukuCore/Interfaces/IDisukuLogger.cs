using DisukuBot.DisukuCore.Entities.Logging;
using System;
using System.Threading.Tasks;

namespace DisukuBot.DisukuCore.Interfaces
{
    public interface IDisukuLogger
    {
        Task LogAsync(DisukuLog logMessage);
        Task LogCriticalAsync(DisukuLog logMessage, Exception exception);
        Task LogCommandAsync(DisukuCommandLog log);
        Task LogCommandAsync(DisukuCommandLog log, string error);
    }
}
