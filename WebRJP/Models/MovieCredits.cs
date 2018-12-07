using System;
using System.Collections.Generic;

namespace WebRJP.Models
{
    public partial class MovieCredits
    {
        public int id { get; set; }
        public int movie_id { get; set; }
        public string credit_id { get; set; }
        public bool is_cast { get; set; }

        public Movies movie_ { get; set; }
        public MovieCasts MovieCasts { get; set; }
        public MovieCrews MovieCrews { get; set; }
    }
}
