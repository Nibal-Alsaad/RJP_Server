using System;
using System.Collections.Generic;

namespace WebRJP.Models
{
    public partial class Movies
    {
        public Movies()
        {
            MovieCredits = new HashSet<MovieCredits>();
            MovieGenres = new HashSet<MovieGenres>();
            MovieKeyWords = new HashSet<MovieKeyWords>();
        }

        public int id { get; set; }
        public string original_language { get; set; }
        public int vote_average { get; set; }
        public string title { get; set; }
        public string poster_path { get; set; }
        public string status { get; set; }
        public DateTime release_date { get; set; }
        public bool? video { get; set; }
        public decimal? popularity { get; set; }
        public string original_title { get; set; }
        public string backdrop_path { get; set; }
        public bool? adult { get; set; }
        public string overview { get; set; }
        public int vote_count { get; set; }
        public int? budget { get; set; }
        public int? revenue { get; set; }
        public int? runtime { get; set; }

        public ICollection<MovieCredits> MovieCredits { get; set; }
        public ICollection<MovieGenres> MovieGenres { get; set; }
        public ICollection<MovieKeyWords> MovieKeyWords { get; set; }
    }
}
