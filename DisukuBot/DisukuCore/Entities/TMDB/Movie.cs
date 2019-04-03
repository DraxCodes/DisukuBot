using System;

namespace DisukuBot.DisukuCore.Entities.TMDB
{
    public class Movie
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string BackdropUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
