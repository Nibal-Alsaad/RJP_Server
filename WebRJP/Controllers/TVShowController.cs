using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebRJP.Common;
using WebRJP.Data.Data.Interfaces.Repositories;
using WebRJP.Models;

namespace WebRJP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TVShowController : ControllerBase
    {
        private readonly ILogger<TVShowController> _logger;
        ITVShowRepository _tvShowRepository;
        public TVShowController(ITVShowRepository tvShowRepository, ILogger<TVShowController> logger)
        {
            _tvShowRepository = tvShowRepository;
            _logger = logger;
        }
        [HttpGet("/api/TVShow/{tvShowId}")]
        public async Task<TVShows> GetTVShowDetails(int tvShowId)
        {
            return await _tvShowRepository.GetDBTVShowDetails(tvShowId);
        }
        [HttpGet("/api/TVShow/AllTVShows")]
        public async Task<IEnumerable<TVShows>> GetAllTVShows()
        {
            return await _tvShowRepository.GetDBAllTVShows();
        }
        [HttpGet("/api/TVShow/{tvShowId}/Genres")]
        public async Task<IEnumerable<Genres>> GetTVShowGenres(int tvShowId)
        {
            return await _tvShowRepository.GetDBTVShowGenres(tvShowId);
        }
        [HttpGet("/api/TVShow/{tvShowId}/KeyWords")]
        public async Task<IEnumerable<KeyWords>> GetTVShowKeyWords(int tvShowId)
        {
            return await _tvShowRepository.GetDBTVShowKeyWords(tvShowId);

        }
        [HttpGet("/api/TVShow/{tvShowId}/Credit")]
        public async Task<TVShowTMDBCredit> GetTVShowCredit(int tvShowId)
        {
            return await _tvShowRepository.GetDBTVShowCredit(tvShowId);
        }
        [HttpPost("/api/TVShow/CreateNewTVShow")]
        // [Authorize]
        public async Task<object> AddFullTVShowDetails([FromBody] FullTVShow fullTVShow)
        {
            try
            {
                return await _tvShowRepository.AddToDBFullTVShowDeails(fullTVShow);
            }
            catch (Exception ex)
            {
                _logger.LogError("create new movie failed");
                throw ex;

            }
        }

    }
}