﻿using System;
using System.Collections.Generic;

namespace WebRJP.Models
{
    public partial class TVShowCrews
    {
        public int id { get; set; }
        public string credit_id { get; set; }
        public string department { get; set; }
        public int gender { get; set; }
        public string job { get; set; }
        public string name { get; set; }
        public string profile_path { get; set; }

        public TVShowCredits credit_ { get; set; }
    }
}
