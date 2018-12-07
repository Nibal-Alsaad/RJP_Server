using System;
using System.Collections.Generic;

namespace WebRJP.Models
{
    public partial class MovieGenres
    {
        public int id { get; set; }
        public int movie_id { get; set; }
        public int movie_genre_id { get; set; }

        public Movies movie_ { get; set; }
        public Genres movie_genre_ { get; set; }
    }
}
