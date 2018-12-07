using System;
using System.Collections.Generic;

namespace WebRJP.Models
{
    public partial class MaterialsType
    {
        public MaterialsType()
        {
            Genres = new HashSet<Genres>();
            KeyWords = new HashSet<KeyWords>();
        }

        public int id { get; set; }
        public string name { get; set; }

        public ICollection<Genres> Genres { get; set; }
        public ICollection<KeyWords> KeyWords { get; set; }
    }
}
