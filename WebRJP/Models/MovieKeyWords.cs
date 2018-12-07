using System;
using System.Collections.Generic;

namespace WebRJP.Models
{
    public partial class MovieKeyWords
    {
        public int id { get; set; }
        public int movie_id { get; set; }
        public int movie_keyword_id { get; set; }

        public Movies movie_ { get; set; }
        public KeyWords movie_keyword_ { get; set; }
    }
}
