using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebRJP.Common;
using WebRJP.Data.Data.Interfaces.Repositories;
using WebRJP.Models;

namespace WebRJP.Data.Data.EF.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        RJPContext _context;
        public MovieRepository(RJPContext context)
        {
            _context = context;
        }
        public Task<IEnumerable<Movies>> GetDBAllMovies()
        {
            return Task.Run(async () => (
            await _context.Movies
            .ToListAsync())
            .AsEnumerable());
        }

        public Task<MovieTMDBCredit> GetDBMovieCredit(int movieId)
        {
            return Task.Run(() =>
            {
                var credits = _context.MovieCredits
                   .Where(c => c.movie_id == movieId)
                .Include(castRes => castRes.MovieCasts)
                .Include(crewRes => crewRes.MovieCrews);
                return new MovieTMDBCredit
                {
                    Id = movieId,
                    cast = credits.Where(c => c.is_cast).Select(item => item.MovieCasts).ToList(),


                    crew = credits.Where(c => !c.is_cast).Select(item => item.MovieCrews).ToList()


                };
            });
        }


        public Task<Movies> GetDBMovieDetails(int movieId)
        {
            return Task.Run(
                async() =>await _context.Movies
                .FirstOrDefaultAsync(movie => movie.id == movieId));
        }

        public Task<IEnumerable<Genres>> GetDBMovieGenres(int movieId)
        {
            return Task.Run(async () =>
            {
                var movie =await _context.Movies.FirstOrDefaultAsync(m => m.id == movieId);
                return (
                  await _context.Entry(movie)
                  .Collection(c => c.MovieGenres)
                  .Query().Select(movieGenres => movieGenres.movie_genre_)
                  .ToListAsync())
                  .AsEnumerable();

            });
        }

        public Task<IEnumerable<KeyWords>> GetDBMovieKeyWords(int movieId)
        {
            return Task.Run(async () =>
            {
                var movie =await _context.Movies.FirstOrDefaultAsync(m => m.id == movieId);
                return (
               await _context.Entry(movie)
                .Collection(c => c.MovieKeyWords)
                .Query().Select(movieKeyWord => movieKeyWord.movie_keyword_).ToListAsync()).AsEnumerable();
            });
        }

        public async Task<FullMovie> AddToDBFullMovieDeails(FullMovie fullMovie)
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                try
                {
                    //AddMovie
                    await _context.Movies.AddAsync(fullMovie.movie);

                   // AddMovieGenres
                    var movieGenres = new List<MovieGenres>();
                    foreach (var item in fullMovie.genres_collection)
                    {
                        movieGenres.Add(new MovieGenres
                        {
                            movie_id = fullMovie.movie.id,
                            movie_genre_id = _context.Genres.FirstOrDefault(g => g.genre_id == item.id && g.material_type_id == 1).id,
                        });
                    }
                    await _context.MovieGenres.AddRangeAsync(movieGenres);

                    //AddKeyWords
                    var KeyWords = new List<KeyWords>();
                    foreach (var item in fullMovie.keyWords_collection)
                    {
                        KeyWords.Add(new KeyWords
                        {
                            keyword_id = item.id,
                            material_type_id = 1,
                            name=item.name
                             
                        });
                    }
                    await _context.KeyWords.AddRangeAsync(KeyWords);
                    await _context.SaveChangesAsync();
                    //AddMovieKeyWords
                    var movieKeyWords = new List<MovieKeyWords>();
                    foreach (var item in fullMovie.keyWords_collection)
                    {
                        var movieKeyWord =await _context.KeyWords.FirstOrDefaultAsync(k => k.keyword_id == item.id && k.material_type_id == 1);
                        movieKeyWords.Add(new MovieKeyWords
                        {                            
                            movie_id = fullMovie.movie.id,
                            movie_ = fullMovie.movie,
                            movie_keyword_ = movieKeyWord ,
                            movie_keyword_id = movieKeyWord.id                                                                             
                        });
                    }
                    await _context.MovieKeyWords.AddRangeAsync(movieKeyWords);

                    //AddMovieCredits
                    var movieCredits = new List<MovieCredits>();
                    foreach (var item in fullMovie.casts_collection)
                    {
                        movieCredits.Add(new MovieCredits
                        {
                            movie_id = fullMovie.movie.id,
                            credit_id = item.credit_id,
                            is_cast = true,
                            movie_=fullMovie.movie
                        });
                    }
                    foreach (var item in fullMovie.crews_collection)
                    {
                        movieCredits.Add(new MovieCredits
                        {
                            movie_id = fullMovie.movie.id,
                            credit_id = item.credit_id,
                            is_cast = false,
                            movie_=fullMovie.movie
                        });
                    }
                    await _context.MovieCredits.AddRangeAsync(movieCredits);
                    await _context.MovieCasts.AddRangeAsync(fullMovie.casts_collection);
                    await _context.MovieCrews.AddRangeAsync(fullMovie.crews_collection);
                    await _context.SaveChangesAsync();
                    trans.Commit();
                    return fullMovie;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message);
                }
                finally
                {
                    trans.Dispose();
                }
            }

        }
    }
}
