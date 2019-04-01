using System;
using System.Collections.Generic;
using System.Text;

namespace DisukuBot.DisukuCore.Entities.RaiderIO
{
    public class Affix
    {
        public string Title { get; set; }
        public List<Segment> Segments { get; set; }

    }

    public class Segment
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }
}
