using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebRJP.Models;

namespace WebRJP.Common
{
    public class MovieTMDBCredit
    {
        public int Id;
        public ICollection<MovieCasts> cast;
        public ICollection<MovieCrews> crew;
    }
    public class TVShowTMDBCredit
    {
        public int Id;
        public ICollection<TVShowCasts> cast;
        public ICollection<TVShowCrews> crew;
    }
}
