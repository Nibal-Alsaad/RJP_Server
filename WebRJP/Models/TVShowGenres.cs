using System;
using System.Collections.Generic;

namespace WebRJP.Models
{
    public partial class TVShowGenres
    {
        public int id { get; set; }
        public int tvshow_id { get; set; }
        public int tvshow_genre_id { get; set; }

        public TVShows tvshow_ { get; set; }
        public Genres tvshow_genre_ { get; set; }
    }
}
