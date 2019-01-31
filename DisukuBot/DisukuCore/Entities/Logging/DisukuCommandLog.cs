using System;
using System.Collections.Generic;
using System.Text;

namespace DisukuBot.DisukuCore.Entities.Logging
{
    public class DisukuCommandLog
    {
        public string User { get; set; }
        public string CommandName { get; set; }
        public string Channel { get; set; }
        public string Guild { get; set; }
    }
}
