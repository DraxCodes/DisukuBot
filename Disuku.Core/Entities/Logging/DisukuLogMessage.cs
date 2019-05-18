namespace Disuku.Core.Entities.Logging
{
    public class DisukuLog
    {
        public string Source { get; set; }
        public string Message { get; set; }
        public DisukuLogSeverity Severity { get; set; }
    }
}
