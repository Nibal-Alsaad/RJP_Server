using System;
using System.Collections.Generic;

namespace WebRJP.Models
{
    public partial class TVShows
    {
        public TVShows()
        {
            TVShowCredits = new HashSet<TVShowCredits>();
            TVShowGenres = new HashSet<TVShowGenres>();
            TVShowKeyWords = new HashSet<TVShowKeyWords>();
        }

        public int id { get; set; }
        public string original_language { get; set; }
        public int vote_average { get; set; }
        public string name { get; set; }
        public string poster_path { get; set; }
        public DateTime first_air_date { get; set; }
        public decimal? popularity { get; set; }
        public string backdrop_path { get; set; }
        public string original_name { get; set; }
        public string overview { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public int vote_count { get; set; }

        public ICollection<TVShowCredits> TVShowCredits { get; set; }
        public ICollection<TVShowGenres> TVShowGenres { get; set; }
        public ICollection<TVShowKeyWords> TVShowKeyWords { get; set; }
    }
}
