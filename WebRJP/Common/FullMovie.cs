using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebRJP.Models;

namespace WebRJP.Common
{
    public class FullMovie
    {
        public Movies movie { get; set; }
        public ICollection<RequestGenreModel> genres_collection { get; set; }
        public ICollection<RequestKeyWordModel> keyWords_collection { get; set; }
        public ICollection<MovieCasts> casts_collection { get; set; }
        public ICollection<MovieCrews> crews_collection { get; set; }
    }
}
