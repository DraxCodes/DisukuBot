using System.Collections.Generic;

namespace Disuku.Core.Entities.TMDB
{
    public class MovieCollection : Movie
    {
        public List<Movie> Movies { get; set; }

    }
}
