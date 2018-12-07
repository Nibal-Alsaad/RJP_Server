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
    public class TVShowRepository : ITVShowRepository
    {
        RJPContext _context;
        public TVShowRepository(RJPContext context)
        {
            _context = context;
        }
        public Task<IEnumerable<TVShows>> GetDBAllTVShows()
        {
            return Task.Run(async()=>(
            await _context
            .TVShows.ToListAsync()).AsEnumerable());
        }

        public Task<TVShowTMDBCredit> GetDBTVShowCredit(int tvShowId)
        {
            return Task.Run(() =>
            {
                var credits = _context.TVShowCredits.Where(credit => credit.tvshow_id == tvShowId)
                      .Include(castRes => castRes.TVShowCasts)
                      .Include(crewRes => crewRes.TVShowCrews);
                return new TVShowTMDBCredit
                {
                    Id = tvShowId,
                    cast = credits.Where(c => c.is_cast).Select(item => item.TVShowCasts).ToList(),
                    crew = credits.Where(c => !c.is_cast).Select(item => item.TVShowCrews).ToList()
                };
            });

        }

        public Task<TVShows> GetDBTVShowDetails(int tvShowId)
        {
            return Task.Run(async () =>
                await _context.TVShows.FirstOrDefaultAsync(tvShow => tvShow.id == tvShowId)
            );
        }

        public Task<IEnumerable<Genres>> GetDBTVShowGenres(int tvShowId)
        {
            return Task.Run(async () =>
            {
                var tvShow =await _context.TVShows.FirstOrDefaultAsync(tv => tv.id == tvShowId);
                return (await _context.Entry(tvShow).Collection(tv => tv.TVShowGenres)
                  .Query().Select(tvGenres => tvGenres.tvshow_genre_)
                  .ToListAsync()).AsEnumerable();
            });
        }

        public async Task<IEnumerable<KeyWords>> GetDBTVShowKeyWords(int tvShowId)
        {
            var tvshow = await _context.TVShows.FirstOrDefaultAsync(tv => tv.id == tvShowId);
            return (await _context.Entry(tvshow).Collection(tv => tv.TVShowKeyWords)
                .Query().Select(tvKeyword => tvKeyword.tvshow_keyword_)
                .ToListAsync()).AsEnumerable();
        }
        public async Task<FullTVShow> AddToDBFullTVShowDeails(FullTVShow fullTVShow)
        {
            using(var trans = _context.Database.BeginTransaction())
            {
                try{
               
            //AddTVShow
            await _context.TVShows.AddAsync(fullTVShow.tvshow);

            //AddTVShowGenres
            var tvShowGenresList = new List<TVShowGenres>();
            foreach(var item in fullTVShow.genres_collection)
            {
                tvShowGenresList.Add(new TVShowGenres
                {
                    tvshow_id = fullTVShow.tvshow.id,
                    tvshow_genre_id = _context.Genres.FirstOrDefault(i => i.genre_id == item.id).id
                });
            }
            await _context.TVShowGenres.AddRangeAsync(tvShowGenresList);

            //AddKeywords
            var keywordsList = new List<KeyWords>();
            foreach(var item in fullTVShow.keyWords_collection)
            {
                keywordsList.Add(new KeyWords
                {
                    keyword_id = item.id,
                    name = item.name,
                    material_type_id = 2
                });
            }
            await _context.KeyWords.AddRangeAsync(keywordsList);

            //AddTVShowKeywords
            var tvShowKeywordList = new List<TVShowKeyWords>();
            foreach(var item in fullTVShow.keyWords_collection)
            {
                var tvShowKeyword =await _context.KeyWords.FirstOrDefaultAsync(k => k.keyword_id == item.id && k.material_type_id==2);
                tvShowKeywordList.Add(new TVShowKeyWords
                {
                    tvshow_id = fullTVShow.tvshow.id,
                    tvshow_ = fullTVShow.tvshow,
                    tvshow_keyword_id = tvShowKeyword.id,
                    tvshow_keyword_ = tvShowKeyword
                });
            }
            await _context.TVShowKeyWords.AddRangeAsync(tvShowKeywordList);

            //AddTVShowCredits
            var TVShowCreditList = new List<TVShowCredits>();
            foreach(var item in fullTVShow.casts_collection)
            {
                TVShowCreditList.Add(new TVShowCredits
                {
                    credit_id = item.credit_id,
                    tvshow_id = fullTVShow.tvshow.id,
                    tvshow_ = fullTVShow.tvshow,
                    is_cast = true
                });
            }
            foreach (var item in fullTVShow.crews_collection)
            {
                TVShowCreditList.Add(new TVShowCredits
                {
                    credit_id = item.credit_id,
                    tvshow_id = fullTVShow.tvshow.id,
                    tvshow_ = fullTVShow.tvshow,
                    is_cast = false
                });
            }
            await _context.TVShowCredits.AddRangeAsync(TVShowCreditList);
            await _context.TVShowCasts.AddRangeAsync(fullTVShow.casts_collection);
            await _context.TVShowCrews.AddRangeAsync(fullTVShow.crews_collection);
            await _context.SaveChangesAsync();
            trans.Commit();
            return fullTVShow;
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
