using System;
using System.Collections.Generic;

namespace WebRJP.Models
{
    public partial class Genres
    {
        public Genres()
        {
            MovieGenres = new HashSet<MovieGenres>();
            TVShowGenres = new HashSet<TVShowGenres>();
        }

        public int id { get; set; }
        public int genre_id { get; set; }
        public string name { get; set; }
        public int material_type_id { get; set; }

        public MaterialsType material_type_ { get; set; }
        public ICollection<MovieGenres> MovieGenres { get; set; }
        public ICollection<TVShowGenres> TVShowGenres { get; set; }
    }
}
