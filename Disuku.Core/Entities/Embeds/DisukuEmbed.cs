using System.Collections.Generic;

namespace Disuku.Core.Entities.Embeds
{
    public class DisukuEmbed
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public string Thumbnail { get; set; }
        public string ImageUrl { get; set; }
        public string Footer { get; set; }
        public List<Field> Fields { get; set; } = new List<Field>();
    }

    public class Field
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
