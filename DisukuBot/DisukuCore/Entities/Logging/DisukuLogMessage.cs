using System;
using System.Collections.Generic;
using System.Text;

namespace DisukuBot.DisukuCore.Entities.Logging
{
    public class DisukuLog
    {
        public string Source { get; set; }
        public string Message { get; set; }
        public DisukuLogSeverity Severity { get; set; }
    }
}
