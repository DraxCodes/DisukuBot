namespace Disuku.Core.Entities.Logging
{
    public class DisukuCommandLog
    {
        public string User { get; set; }
        public string CommandName { get; set; }
        public string Channel { get; set; }
        public string Guild { get; set; }
    }
}
