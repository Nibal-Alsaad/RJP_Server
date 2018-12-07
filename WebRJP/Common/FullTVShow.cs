using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebRJP.Models;

namespace WebRJP.Common
{
    public class FullTVShow
    {
        public TVShows tvshow { get; set; }
        public ICollection<RequestGenreModel> genres_collection { get; set; }
        public ICollection<RequestKeyWordModel> keyWords_collection { get; set; }
        public ICollection<TVShowCasts> casts_collection { get; set; }
        public ICollection<TVShowCrews> crews_collection { get; set; }
    }
}
