using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebRJP.Common;
using WebRJP.Models;

namespace WebRJP.Data.Data.Interfaces.Repositories
{
   public interface ITVShowRepository
    {
        Task<IEnumerable<TVShows>> GetDBAllTVShows();
        Task<TVShows> GetDBTVShowDetails(int tvShowId);
        Task<IEnumerable<Genres>> GetDBTVShowGenres(int tvShowId);
        Task<TVShowTMDBCredit> GetDBTVShowCredit(int tvShowId);
        Task<IEnumerable<KeyWords>> GetDBTVShowKeyWords(int tvShowId);
        Task<FullTVShow> AddToDBFullTVShowDeails(FullTVShow fullTVShow);
    }
}
