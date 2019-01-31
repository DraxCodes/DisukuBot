using DisukuBot.DisukuCore.Entities.Logging;
using System;
using System.Threading.Tasks;

namespace DisukuBot.DisukuCore.Interfaces
{
    public interface IDisukuLogger
    {
        Task LogAsync(DisukuLogMessage logMessage);
        Task LogCriticalAsync(DisukuLogMessage logMessage, Exception exception);
    }
}
