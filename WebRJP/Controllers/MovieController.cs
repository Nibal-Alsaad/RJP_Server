using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebRJP.Common;
using WebRJP.Data.Data.Interfaces.Repositories;
using WebRJP.Models;

namespace WebRJP.Controllers
{
    [Route("api/[controller]")]
    // [ApiController]
    public class MovieController : Controller
    {
        private readonly ILogger<MovieController> _logger;
        IMovieRepository _movieRepository;
        public MovieController(IMovieRepository movieRepository, ILogger<MovieController> logger)
        {
            _movieRepository = movieRepository;
            _logger = logger;
        }
        [HttpGet("/api/Movie/{movieId}")]
        public async Task<Movies> GetMovieDetails(int movieId)
        {
            return await _movieRepository.GetDBMovieDetails(movieId);
        }
        [HttpGet("/api/Movie/AllMovies")]
        public async Task<IEnumerable<Movies>> GetAllMovies()
        {
            return await _movieRepository.GetDBAllMovies();
        }
        [HttpGet("/api/Movie/{movieId}/Genres")]
        public async Task<IEnumerable<Genres>> GetMovieGenres(int movieId)
        {
            return await _movieRepository.GetDBMovieGenres(movieId);
        }
        [HttpGet("/api/Movie/{movieId}/KeyWords")]
        public async Task<IEnumerable<KeyWords>> GetMovieKeyWords(int movieId)
        {
            return await _movieRepository.GetDBMovieKeyWords(movieId);

        }
        [HttpGet("/api/Movie/{movieId}/Credit")]
        public async Task<MovieTMDBCredit> GetMovieCredit(int movieId)
        {
            return await _movieRepository.GetDBMovieCredit(movieId);
        }
        [HttpPost("/api/Movie/CreateNewMovie")]
        // [Authorize]
        public async Task<object> AddFullMovieDetails([FromBody] Newtonsoft.Json.Linq.JObject fullMovie)
        {
            try
            {
                var movieDetails= fullMovie.ToObject<FullMovie>();
                return await _movieRepository.AddToDBFullMovieDeails(movieDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError("create new movie failed");
                throw ex;

            }
        }

    }
}