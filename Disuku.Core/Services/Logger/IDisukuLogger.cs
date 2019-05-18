using Disuku.Core.Entities.Logging;
using System;
using System.Threading.Tasks;

namespace Disuku.Core.Services.Logger
{
    public interface IDisukuLogger
    {
        Task LogAsync(DisukuLog logMessage);
        Task LogCriticalAsync(DisukuLog logMessage, Exception exception);
        Task LogCommandAsync(DisukuCommandLog log);
        Task LogCommandAsync(DisukuCommandLog log, string error);
    }
}
