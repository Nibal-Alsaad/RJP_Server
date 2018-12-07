using System;
using System.Collections.Generic;

namespace WebRJP.Models
{
    public partial class TVShowCredits
    {
        public int id { get; set; }
        public int tvshow_id { get; set; }
        public string credit_id { get; set; }
        public bool is_cast { get; set; }

        public TVShows tvshow_ { get; set; }
        public TVShowCasts TVShowCasts { get; set; }
        public TVShowCrews TVShowCrews { get; set; }
    }
}
