using System;
using System.Collections.Generic;

namespace WebRJP.Models
{
    public partial class TVShowKeyWords
    {
        public int id { get; set; }
        public int tvshow_id { get; set; }
        public int tvshow_keyword_id { get; set; }

        public TVShows tvshow_ { get; set; }
        public KeyWords tvshow_keyword_ { get; set; }
    }
}
