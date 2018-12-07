using System;
using System.Collections.Generic;

namespace WebRJP.Models
{
    public partial class KeyWords
    {
        public KeyWords()
        {
            MovieKeyWords = new HashSet<MovieKeyWords>();
            TVShowKeyWords = new HashSet<TVShowKeyWords>();
        }

        public int id { get; set; }
        public int keyword_id { get; set; }
        public string name { get; set; }
        public int material_type_id { get; set; }

        public MaterialsType material_type_ { get; set; }
        public ICollection<MovieKeyWords> MovieKeyWords { get; set; }
        public ICollection<TVShowKeyWords> TVShowKeyWords { get; set; }
    }
}
