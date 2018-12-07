using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebRJP.Common;
using WebRJP.Models;

namespace WebRJP.Data.Data.Interfaces.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movies>> GetDBAllMovies();
        Task<Movies> GetDBMovieDetails(int movieId);
        Task<IEnumerable<Genres>> GetDBMovieGenres(int movieId);
        Task<MovieTMDBCredit> GetDBMovieCredit(int movieId);
        Task<IEnumerable<KeyWords>> GetDBMovieKeyWords(int movieId);
        Task<FullMovie> AddToDBFullMovieDeails(FullMovie fullMovie);
    }
}
