using HW4.Models;
using HW4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HW4.Controllers
{
    [Route("api/movie")]
    [ApiController]
    public class TmdbApiController : ControllerBase
    {
        private readonly ITMDBService _tmdbService;
        private readonly ILogger<TmdbApiController> _logger;

        public TmdbApiController(ITMDBService tmdbService, ILogger<TmdbApiController> logger)
        {
            _tmdbService = tmdbService;
            _logger = logger;
        }

        // GET: api/movie/search?title=titanic
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<Movie>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<Movie>>> SearchMoviesAsync(string title)
        {
            try
            {
                IEnumerable<Movie> movies = await _tmdbService.SearchMoviesAsync(title);
                return Ok(movies);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error searching TMDB movies");
                return StatusCode(204, "Service Unavailable");
            }
        }

        // GET: api/movie/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MovieDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<MovieDetails>> GetMovieDetailsAsync(int id)
        {
            try
            {
                MovieDetails movie = await _tmdbService.GetMovieDetailsAsync(id);
                return Ok(movie);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting TMDB movie details");
                return StatusCode(204, "Service Unavailable");
            }
        }

        // GET: api/movie/5/credits
        [HttpGet("{id}/credits")]
        [ProducesResponseType(typeof(MovieCredits), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<MovieCredits>> GetMovieCreditsAsync(int id)
        {
            try
            {
                MovieCredits credits = await _tmdbService.GetMovieCreditsAsync(id);
                return Ok(credits);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting TMDB movie credits");
                return StatusCode(204, "Service Unavailable");
            }
        }
    }
}